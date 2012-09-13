using System;
using System.Threading;
using System.Windows.Forms;
using ASK.ServEasy.Windows.Log;
using ASK.ServEasy.Windows.WinForms;

namespace ASK.ServEasy.Windows.Containers
{
	public class WinFormContainer : IModuleContainer
	{
		#region Log4net Declaration
		private static readonly log4net.ILog theLogger = log4net.LogManager.GetLogger(typeof(WinFormContainer));
		#endregion

		private Thread theWinFormThread;
		private bool isConfiguring;
		private readonly LogManagerAppender theLogAppender;
		private ApplicationContext theApplicationContext;

		public WinFormContainer()
		{
			theLogAppender = new LogManagerAppender();
			log4net.Config.BasicConfigurator.Configure(theLogAppender);
			((log4net.Repository.Hierarchy.Hierarchy)log4net.LogManager.GetRepository()).ConfigurationChanged += ConfigurationChanged;
		}

		public Module Module { get; private set; }

		public void PushLogMessages()
		{
			theLogAppender.PushLogMessages();
		}

		public void Initialize(Module aModule)
		{
			Module = aModule;
			theWinFormThread = new Thread(WinFormThread);
			theWinFormThread.SetApartmentState(ApartmentState.STA);
			theWinFormThread.Start();
		}

		private void WinFormThread()
		{
			Thread.CurrentThread.Name = "WinForm Thread";
			Application.EnableVisualStyles();

			try
			{
				ModuleForm f = new ModuleForm {ModuleContainer = this};

				theApplicationContext = new ApplicationContext(f);

				Application.Run(theApplicationContext);
				theLogger.DebugFormat("WinForm Thread Terminated");
			}
			catch (Exception ex)
			{
				theLogger.Error("Error", ex);
			}
		}

		private void ConfigurationChanged(object sender, EventArgs e)
		{
			// If we aren't configuring log4net appender nor closing window, add control as IAppender
			if (!(isConfiguring))
			{
				isConfiguring = true;
				log4net.Config.BasicConfigurator.Configure(theLogAppender);
				isConfiguring = false;
			}
		}

		public void Dispose()
		{
			theLogger.DebugFormat("Disposing...");
			theApplicationContext.ExitThread();
			theApplicationContext = null;
		}
	}
}
