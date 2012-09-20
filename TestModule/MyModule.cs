using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestModule
{
	class MyModule : ASK.ServEasy.Module
	{
		#region log4net declaration
		private static readonly log4net.ILog theLogger = log4net.LogManager.GetLogger(typeof(MyModule));
		#endregion

		public MyModule()
		{
			//base.Schedule = "* * * * *";
			base.WatchdogDelay = TimeSpan.FromSeconds(50);
		}

		protected override void Initializing()
		{
			StatusMessage = "Module Initialized";
		}
		protected override void Starting()
		{
			StatusMessage = "Module I'm starting...";
			for (int i = 0; i < 5; i++)
			{
				if (!MustRun)
					break;

				AddThread(new MyModuleThread(i));
			}
			StatusMessage = "Module Started";
		}

		protected override void Running()
		{
			StatusMessage = "Module I'm running...";
			Sleep(TimeSpan.FromMilliseconds(1000));
			StatusMessage = "Running completed";
		}
		protected override void Stopping()
		{
			StatusMessage = "Module STOPPING...";

			foreach (var moduleThread in Threads)
			{
				RemoveThread(moduleThread);
			}
		}
	}
}
