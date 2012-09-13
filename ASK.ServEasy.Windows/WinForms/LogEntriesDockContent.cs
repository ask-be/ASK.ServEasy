using System;
using System.Windows.Forms;
using ASK.ServEasy.Windows.Log;
using WeifenLuo.WinFormsUI.Docking;

namespace ASK.ServEasy.Windows.WinForms
{
	public partial class LogEntriesDockContent : DockContent
	{
		public LogEntriesDockContent()
		{
			InitializeComponent();

			base.Text = "Logs";
		}

		public TextBox DetailsTextBox { get; set; }

		public ListView ListView
		{
			get { return theLoggerListView; }
		}

		private void theLoggerListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (theLoggerListView.SelectedIndices.Count > 0)
			{
				LogMessageItem msg = theLoggerListView.SelectedItems[0].Tag as LogMessageItem;
				msg.Parent.Highlight = true;

				if (DetailsTextBox != null)
				{
					DetailsTextBox.Text = msg.Message.ToString();
				}
			}
		}
	}
}
