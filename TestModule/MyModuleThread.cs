using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASK.ServEasy;

namespace TestModule
{
	class MyModuleThread : ModuleThread
	{
		public MyModuleThread(int i)
			: base("My Module Thread -" + i,singleRun:true)
		{
			WatchdogDelay = TimeSpan.FromSeconds(2);
		}

		protected override void Initializing()
		{
			StatusMessage = "ModuleThread Initialized";
			
		}
		protected override void Starting()
		{
			StatusMessage = "ModuleThread I'm starting...";
		}
		protected override void Running()
		{
			StatusMessage = "ModuleThread I'm running...";
			
			if (!Sleep(new TimeSpan(0, 0, 10)))
			{
				return;
			}
			StatusMessage = "Waiting next iteration...";
		}
		protected override void Stopping()
		{
			StatusMessage = "ModuleThread I'm stopping...";
		}
	}

	class MyModuleThread2 : ModuleThread
	{
		#region log4net declaration
		private static readonly log4net.ILog theLogger = log4net.LogManager.GetLogger(typeof(MyModuleThread2));
		#endregion

		public MyModuleThread2()
			: base("My Module Thread 2")
		{
			//base.Schedule = "0 * * * *";
		}

		protected override void Initializing()
		{
			StatusMessage = "ModuleThread Initialized";
		}
		protected override void Starting()
		{
			StatusMessage = "ModuleThread I'm starting...";
		}
		protected override void Running()
		{
			StatusMessage = "ModuleThread I'm running...";
			while (Sleep(TimeSpan.FromMilliseconds(50)))
			{
				theLogger.Info("Coucou");
			}

		}
		protected override void Stopping()
		{
			StatusMessage = "ModuleThread I'm stopping...";
		}
	}
}
