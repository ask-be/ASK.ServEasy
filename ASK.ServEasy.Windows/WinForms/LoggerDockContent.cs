using System.Windows.Forms;
using ASK.ServEasy.Windows.Log;
using WeifenLuo.WinFormsUI.Docking;

namespace ASK.ServEasy.Windows.WinForms
{
	public partial class LoggerDockContent : DockContent
	{
		private bool _ignoreEvents;

		public LoggerDockContent()
		{
			InitializeComponent();

			LoggerView = new TreeViewLoggerView(treeView1);
		}

		public ILoggerView LoggerView
		{
			get;
			private set;
		}

		private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
		{
			using (new AutoWaitCursor())
			{
				//
				// If we are suppose to ignore events right now, then just
				// return.
				//
				if (_ignoreEvents) return;

				try
				{
					//
					// Set a flag to ignore future events while processing this event. We have
					// to do this because it may be possbile that this event gets fired again
					// during a recursive call. To avoid more processing than necessary, we should
					// set a flag and clear it when we're done.
					//
					_ignoreEvents = true;

					//
					// Enable/disable the logger item that is represented by the
					// checked node.
					//
					(e.Node.Tag as LoggerItem).Enabled = e.Node.Checked;
				}
				finally
				{
					_ignoreEvents = false;
				}
			}
		}
	}
}
