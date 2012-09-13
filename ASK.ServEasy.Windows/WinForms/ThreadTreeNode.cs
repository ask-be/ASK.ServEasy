using System;
using Aga.Controls.Tree;

namespace ASK.ServEasy.Windows.WinForms
{
	class ThreadTreeNode : Node
	{
		private ModuleThread theThread;

		public ThreadTreeNode(ModuleThread aThread)
		{
			theThread = aThread;
			theThread.ScheduleChanged += NotifyModel;
			theThread.StateChanged += NotifyModel;
			theThread.StatusMessageChanged += NotifyModel;
		}

		void NotifyModel(object sender, ModuleThreadEventArgs e)
		{
			NotifyModel();
		}

		public string Name { get { return theThread.Name; } }
		public string State { get { return theThread.State.ToString(); } }
		public string StatusMessage { get { return theThread.StatusMessage; } }
		public string NextRun { get { return theThread.NextRun.HasValue ? theThread.NextRun.Value.ToString() : String.Empty; } }
		public string Schedule { get { return theThread.Schedule; } }
	}
}
