using System;
using System.Collections.Generic;

namespace ASK.ServEasy
{
	class Controller
	{
		#region Logger Declaration
		private static readonly Logger.ILog theLogger = Logger.CreateLog(); 
		#endregion

		private IModuleContainer theContainer;
		private bool MustRun { get; set; }
		private readonly List<IModuleController> theModuleControllers = new List<IModuleController>();

		public Controller(Module aModule, IModuleContainer aContainer)
		{
			if (aModule == null)
			{
				theLogger.Fatal("Parameter 'aModule' cannot be null");
				throw new ArgumentNullException("aModule");
			}
			if (aContainer == null)
			{
				theLogger.Fatal("Parameter 'aContainer' cannot be null");
				throw new ArgumentNullException("aContainer");
			}

			Module = aModule;
			theContainer = aContainer;
		}

		internal void Run()
		{
			MustRun = true;
			Module.Initialize(this);
			theContainer.Initialize(Module);

			foreach (IModuleController moduleController in theModuleControllers)
				moduleController.Initialize(Module);

			if(Module.AutoStart)
				Module.Start();

			while (MustRun)
			{
				try
				{
					CheckThread(Module);
				}
				catch (Exception ex)
				{
					theLogger.Warn("Watchdog test failed", ex);
				}
				System.Threading.Thread.Sleep(1000);
			}
		}

		private static void CheckThread(ModuleThread aThread)
		{
			if ((aThread.WatchdogEnabled) && (aThread.State == RunState.Started))
			{
				theLogger.DebugFormat("Watchdog test on thread '{0}'", aThread.Name);
				if (DateTime.Now - aThread.LastWatchdogReset > aThread.WatchdogDelay)
				{
					theLogger.Error("Watchdog timeout !!! Killing and restarting thread " + aThread.Name + "...");
					aThread.Restart();
				}
				else
				{
					foreach (ModuleThread subThread in aThread.Threads)
					{
						CheckThread(subThread);
					}
				}
			}
		}

		private Module Module { get; set; }

		public void Dispose()
		{
			theLogger.Debug("Disposing Controller...");
			MustRun = false;

			foreach (IModuleController controller in theModuleControllers)
			{
				theLogger.DebugFormat("Disposing ModuleController {0}", controller.GetType().FullName);
				controller.Dispose();
			}

			theLogger.DebugFormat("Disposing ModuleContainer {0}",theContainer.GetType().FullName);
			theContainer.Dispose();
			theContainer = null;
		}
	}
}
