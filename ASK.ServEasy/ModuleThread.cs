using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Threading;
using ASK.ServEasy.NCrontab;

namespace ASK.ServEasy
{
	public class ModuleThreadEventArgs : EventArgs
	{
		public ModuleThread Thread { get; set; }
	}

	public class ModuleThread : IDisposable
	{
		#region Logger Declaration
		private static readonly Logger.ILog theLogger = Logger.CreateLog();
		#endregion

		private readonly string theName;
		private readonly Timer theTimer;
		private readonly AutoResetEvent theEvent;
		private readonly List<ModuleThread> theThreads = new List<ModuleThread>();
		private readonly ApartmentState theApartmentState;
		private readonly bool isSingleRun;

		private Thread theThread;
		private string theStatus;
		private CrontabSchedule theSchedule;
		private DateTime theLastRun;
		private RunState theState;

		public event EventHandler<ModuleThreadEventArgs> StateChanged;
		public event EventHandler<ModuleThreadEventArgs> StatusMessageChanged;
		public event EventHandler<ModuleThreadEventArgs> ScheduleChanged;
		public event EventHandler<ModuleThreadEventArgs> ThreadAdded;
		public event EventHandler<ModuleThreadEventArgs> ThreadRemoved;

		protected ModuleThread(string aName, ApartmentState anAparmentState = ApartmentState.MTA, bool autoStart = true, bool singleRun = false )
		{
			Id = Guid.NewGuid().ToString("N");
			theName = aName;
			theApartmentState = anAparmentState;
			AutoStart = autoStart;
			isSingleRun = singleRun;

			theEvent = new AutoResetEvent(false);
			theTimer = new Timer(x => theEvent.Set(), null, TimeSpan.FromMilliseconds(-1), TimeSpan.FromMilliseconds(-1));
			//TODO: Read from file ?
			theLastRun = DateTime.Now;
			CreationDate = DateTime.Now;

			WatchdogDelay = TimeSpan.FromSeconds(30);
		}

		~ModuleThread()
		{
			Dispose(false);
		}

		public string Id { get; private set; }
		public string Schedule
		{
			get { return theSchedule != null ? theSchedule.ToString() : String.Empty; }
			set
			{
				theSchedule = CrontabSchedule.Parse(value);
				RescheduleTimer();
				OnScheduleChanged();
			}
		}
		public string Name { get { return theName; } }
		public RunState State
		{
			get { return theState; }
			private set 
			{
				theState = value;
				OnStateChanged();
			}
		}
		public string StatusMessage
		{
			get { return theStatus; }
			protected set
			{
				theStatus = value;
				OnStatusMessageChanged();
			}
		}

		public bool AutoStart { get; private set; }
		public ModuleThread ParentThread { get; private set; }
		public DateTime CreationDate { get; private set; }
		public DateTime? StartingDate { get; private set; }
		public DateTime LastWatchdogReset { get; private set; }
		public DateTime? NextRun { get; private set; }
		public TimeSpan WatchdogDelay { get; protected set; }
		public IEnumerable<ModuleThread> Threads
		{
			get
			{
				//Create a Copy to avoid list enumeration being broken 
				//if a thread is removed of added from list during enumeration
				List<ModuleThread> threads;
				lock (theThreads)
				{
					threads = theThreads.ToList();
				}
				return threads;
			}
		}

		internal protected bool MustRun { get; private set; }

		internal bool WatchdogEnabled { get; private set; }
		internal void Initialize()
		{
			if (State != RunState.NotInitialized)
				// Invalid State
				return;

			State = RunState.Initializing;
			WatchdogEnabled = true; // Watchdog enabled by default
			Initializing();
			MustRun = false;
			State = RunState.Stopped;
		}

		/// <summary>
		/// Start the Thread and all Inner Thread
		/// </summary>
		public void Start()
		{
			if (State != RunState.Stopped)
			{
				//TODO: Error
				return;
			}

			theLogger.InfoFormat("Starting ModuleThread '{0}'...",Name);

			MustRun = true;
			State = RunState.Starting;

			theThread = new Thread(WorkerThreadProc);
			theThread.SetApartmentState(theApartmentState);
			theThread.Name = ParentThread != null ? String.Format("{0} > {1}", ParentThread.Name,theName) : theName;
			theThread.Start();
		}
		/// <summary>
		/// Stop the Thread
		/// </summary>
		public void Stop()
		{
			if (State == RunState.Stopping || State == RunState.Stopped || State == RunState.Terminated)
			{
				theLogger.InfoFormat("Module Thread '{0}' is already stopped or stopping", Name);
				return;
			}

			theLogger.InfoFormat("Stopping ModuleThread '{0}'...", Name);
			State = RunState.Stopping;

			MustRun = false;

			if (theThread == Thread.CurrentThread)
			{
				Stopping();
			}
			else
			{
				if (theThread.ThreadState != ThreadState.Stopped)
				{
					// Just in case we are waiting for next run
					theEvent.Set();

					// Wait 30 seconds before killing thread
					theThread.Join(new TimeSpan(0, 0, 30));
				}

				// The thread may already have stopped here during the 30 seconds delay
				if (State == RunState.Stopped)
					return;

				if (theThread.ThreadState != ThreadState.Stopped)
				{
					theLogger.InfoFormat("Module Thread {0} not stopped, aborting...", Name);
					theThread.Abort();
					theThread.Join();
				}
			}
			State = RunState.Stopped;
			theThread = null;

			theLogger.InfoFormat("ModuleThread '{0}' Stopped",theName);
		}
		/// <summary>
		/// Restart the Thread
		/// </summary>
		public void Restart()
		{
			Stop();
			Start();
		}
		/// <summary>
		/// Block the calling Thread until this thread terminates or specified time elapses.
		/// </summary>
		/// <param name="aTimeOut"></param>
		public bool Join(TimeSpan aTimeOut)
		{
			if (State != RunState.Started)
				//Invalid State
				return true;

			return theThread.Join(aTimeOut);
		}
		/// <summary>
		/// Release all resources from this ModuleThread
		/// The Thread will be stopped if running.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				theLogger.Debug("Disposing...");
				if (State != RunState.Stopped)
				{
					theLogger.Debug("Stopping...");
					Stop();
					theLogger.Debug("Stopped");
				}
				State = RunState.Terminated;
				theEvent.Close();
				theTimer.Dispose();
				theLogger.DebugFormat("Disposed");
			}
		}
		/// <summary>
		/// Add a sub thread
		/// </summary>
		/// <param name="moduleThread">The thread</param>
		protected void AddThread(ModuleThread moduleThread)
		{
			if (theThreads.Contains(moduleThread))
				return;

			theLogger.DebugFormat("Adding Thread '{0}'",moduleThread.Name);

			moduleThread.ParentThread = this;
			OnThreadAdded(moduleThread);

			moduleThread.Initialize();

			lock (theThreads)
			{
				theThreads.Add(moduleThread);
			}

			// If we are already running and the thread is marked as autostart, let's start it
			if(moduleThread.AutoStart)
			{
				theLogger.DebugFormat("Starting newly added Thread '{0}'",moduleThread.Name);
				moduleThread.Start();
			}
		}
		/// <summary>
		/// Remove a sub thread
		/// </summary>
		/// <param name="moduleThread">The thread</param>
		protected void RemoveThread(ModuleThread moduleThread)
		{
			theLogger.DebugFormat("Removing Thread '{0}'", moduleThread.Name);

			if(moduleThread.State != RunState.Stopped)
			{
				moduleThread.Stop();
			}

			lock (theThreads)
			{
				theThreads.Remove(moduleThread);
			}

			OnThreadRemoved(moduleThread);

			moduleThread.Dispose();
		}
		/// <summary>
		/// Sleep the current Thread for the specified time. If the thread has been stopped during the sleep, the sleep is interrupted
		/// </summary>
		/// <param name="aTimeout"></param>
		/// <returns>true if sleep has not been interrupted</returns>
		protected bool Sleep(TimeSpan aTimeout)
		{
			double delay = aTimeout.TotalMilliseconds;
			while (MustRun && delay > 0)
			{
				ResetWatchdog();
				Thread.Sleep(100);
				delay -= 100;
			}
			return MustRun;
		}

		protected virtual void Initializing()
		{ }
		protected virtual void Starting()
		{ }
		protected virtual void Running()
		{ }
		protected virtual void Stopping()
		{ }
		protected void ResetWatchdog()
		{
			LastWatchdogReset = DateTime.Now;
		}

		/// <summary>
		/// Change the current thread user.
		/// This method must be called after Initialization
		/// </summary>
		/// <param name="aUserName"></param>
		/// <param name="aDomain"></param>
		/// <param name="aPassword"></param>
		protected WindowsImpersonationContext Impersonate(string aUserName, string aDomain, string aPassword)
		{
			if(Environment.OSVersion.Platform.ToString() == "Win32NT")
				return Win32.Impersonate(aUserName, aDomain, aPassword);
			theLogger.WarnFormat("Impersonation is not supported under Linux/MacOSX");
			return null;
		}

		private void RescheduleTimer()
		{
			if (theSchedule != null)
			{
				theLogger.Debug("Rescheduling Timer");
				NextRun = theSchedule.GetNextOccurrence(theLastRun);
				theLogger.DebugFormat("Next run scheduled at {0}", NextRun);
				if (DateTime.Now > NextRun)
				{
					// Now !
					theTimer.Change(0, -1);
				}
				else
				{
					theTimer.Change(NextRun.Value.Subtract(DateTime.Now), TimeSpan.FromMilliseconds(-1));
				}
			}
		}

		/// <summary>
		/// The Thread Proc
		/// </summary>
		private void WorkerThreadProc()
		{
			try
			{


				ThreadStarting();

				while (MustRun)
				{
					WatchdogEnabled = false;

					// Wait for Timer event
					if (theSchedule != null)
						theEvent.WaitOne();

					WatchdogEnabled = true;

					if (!MustRun)
						break;

					ThreadRunning();

					// if singleRun Let's break immediately
					if (!MustRun || isSingleRun)
						break;

					if (theSchedule == null)
					{
						// Sleep at least some milliseconds before calling Running again
						Sleep(TimeSpan.FromMilliseconds(100));
					}
					else
					{
						// Add some seconds to ensure that it will not run too fast
						theLastRun = DateTime.Now.AddSeconds(1);
						RescheduleTimer();
					}
				}

				ThreadStopping();

				if ((ParentThread != null) && (isSingleRun))
					ParentThread.RemoveThread(this);
			}
			catch (Exception)
			{
				theLogger.Warn("Module Thread stopped unexpectedly");
			}
		}

		private void ThreadRunning()
		{
			try
			{
				ResetWatchdog();
				theLogger.Debug("Running...");

				Running();

				theLogger.Debug("Running Completed");
				ResetWatchdog();
			}
			catch(Exception ex)
			{
				theLogger.Error("Error while running",ex);
			}
		}
		private void ThreadStopping()
		{
			try
			{
				theLogger.Debug("Stopping...");
				State = RunState.Stopping;

				while(true) // Stopping Thread without locking the whole Threads list because, threads are remove from that list while stopping
				{
					ModuleThread moduleThead = null;
					lock(theThreads)
					{
						if (theThreads.Count > 0)
							moduleThead = theThreads.FirstOrDefault(x => x.State == RunState.Started);
					}

					if (moduleThead == null)
						break;

					theLogger.DebugFormat("Stopping Sub Thread {0}", moduleThead.Name);
					ResetWatchdog();
					moduleThead.Stop();
					ResetWatchdog();
				}

				Stopping();
				State = RunState.Stopped;
				theLogger.Info("Stopped");
			}
			catch(Exception ex)
			{
				theLogger.Error("Error while stopping",ex);
				throw;
			}
		}
		private void ThreadStarting()
		{
			try
			{
				theLogger.Info("Starting...");

				State = RunState.Starting;
				Starting();

				WindowsIdentity identity = WindowsIdentity.GetCurrent();
				
				if (identity != null)
				{
					theLogger.InfoFormat("Current User : {0}", identity.Name);
				}

				
				lock(theThreads)
				{
					foreach(ModuleThread moduleThead in theThreads.Where(moduleThead => moduleThead.AutoStart))
					{
						theLogger.DebugFormat("Auto Starting Sub Thread {0}", moduleThead.Name);
						ResetWatchdog();
						moduleThead.Start();
						ResetWatchdog();
					}
				}

				theLogger.Info("Started");
				State = RunState.Started;
				StartingDate = DateTime.Now;
			}
			catch(Exception ex)
			{
				theLogger.Error("Error while Starting Thread",ex);
				throw;
			}
		}

		private void OnStateChanged()
		{
			if (StateChanged != null)
				StateChanged(this, new ModuleThreadEventArgs{Thread = this});
		}
		private void OnScheduleChanged()
		{
			if (ScheduleChanged != null)
				ScheduleChanged(this, new ModuleThreadEventArgs { Thread = this });
		}
		private void OnStatusMessageChanged()
		{
			theLogger.DebugFormat("Status changed to '{0}'", StatusMessage);
			if (StatusMessageChanged != null)
				StatusMessageChanged(this, new ModuleThreadEventArgs { Thread = this });
		}
		private void OnThreadAdded(ModuleThread aThread)
		{
			theLogger.DebugFormat("Thread Added");
			if (ThreadAdded != null)
				ThreadAdded(this, new ModuleThreadEventArgs { Thread = aThread });
		}
		private void OnThreadRemoved(ModuleThread aThread)
		{
			theLogger.DebugFormat("Thread Removed");
			if (ThreadRemoved != null)
				ThreadRemoved(this, new ModuleThreadEventArgs { Thread = aThread });
		}
	}
}
