using System.ServiceProcess;
using System.Threading;

namespace ASK.ServEasy.Windows.Containers
{
	class ServiceContainer : ServiceBase, IModuleContainer
	{
		private Module theModule;
		private Thread theServiceThread;

		public void Initialize(Module aModule)
		{
			theModule = aModule;
			theServiceThread = new Thread(ServiceThread);
			theServiceThread.Start();
		}

		protected override void OnStart(string[] args)
		{
			while (theModule.State == RunState.Initializing)
				Thread.Sleep(100);
			theModule.Start();
		}

		protected override void OnStop()
		{
			theModule.Dispose();
			theModule = null;
		}

		private void ServiceThread()
		{
			Thread.CurrentThread.Name = "Service";
			Run(this);
		}
	}
}
