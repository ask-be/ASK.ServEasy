using System;
using System.ComponentModel;
using System.Web.UI;
using System.Windows.Forms;
using ASK.ServEasy.Windows.Containers;
using ASK.ServEasy.Windows.Log;
using ASK.ServEasy.Windows.Services;
using WeifenLuo.WinFormsUI.Docking;

namespace ASK.ServEasy.Windows.WinForms
{
	public partial class ModuleForm : Form
	{
		private ServiceInstaller theServiceInstaller;
		private bool isClosing;

		public WinFormContainer ModuleContainer { get; set; }
		
		public ModuleForm()
		{
			isClosing = false;
			InitializeComponent();
			theDockPanel.Theme = new VS2005Theme();
		}

		private void ModuleForm_Load(object sender, EventArgs e)
		{
			Text = String.Format("{0} ({1})", ModuleContainer.Module.ModuleInfo.ModuleName, ModuleContainer.Module.ModuleInfo.Version);
			try
			{
				theServiceInstaller = new ServiceInstaller(ModuleContainer.Module.ModuleInfo);
				RefreshServiceMenuButtons();
			}
			catch(Exception)
			{
				uninstallWindowsServiceToolStripMenuItem.Enabled = false;
				installAsWindowsServiceToolStripMenuItem.Enabled = false;
			}

			ModuleContainer.Module.StateChanged += Module_StateChanged;

			//TODO: Cleanup here
			UserSettings.Load();

			LogEntriesDockContent content = new LogEntriesDockContent();
			LoggerDockContent logger = new LoggerDockContent();
			LogEntryDetailsDockContent details = new LogEntryDetailsDockContent();
			content.DetailsTextBox = details.theDetailsTextBox;

			ThreadsDockContent threads = new ThreadsDockContent {Module = ModuleContainer.Module};

			LogManager.Instance.Initialize(logger.LoggerView, content.ListView);

			details.Show(theDockPanel, DockState.DockBottomAutoHide);
			logger.Show(theDockPanel, DockState.DockRightAutoHide);
			threads.Show(theDockPanel, DockState.Document);
			content.Show(theDockPanel, DockState.Document);

			// Initialize Module State
			ModuleStateChanged();
		}

		void Module_StateChanged(object sender, ModuleThreadEventArgs e)
		{
			if (!isClosing)
				BeginInvoke(new MethodInvoker(ModuleStateChanged));
		}

		private void RefreshServiceMenuButtons()
		{
			if (theServiceInstaller.ServiceExists)
			{
				uninstallWindowsServiceToolStripMenuItem.Enabled = true;
				installAsWindowsServiceToolStripMenuItem.Enabled = false;
			}
			else
			{
				uninstallWindowsServiceToolStripMenuItem.Enabled = false;
				installAsWindowsServiceToolStripMenuItem.Enabled = true;
			}
		}

		private void theLogTimer_Tick(object sender, EventArgs e)
		{
			ModuleContainer.PushLogMessages();
		}

		private void ModuleForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			isClosing = true;
			// Releasing Module will stop everything
			IAsyncResult res = BeginInvoke(new MethodInvoker(ModuleContainer.Module.Dispose));
			EndInvoke(res);
		}

		private void ModuleStateChanged()
		{
			switch (ModuleContainer.Module.State)
			{
				case RunState.Initializing:
				case RunState.NotInitialized:
				case RunState.Starting:
				case RunState.Stopping:
				case RunState.Terminating:
				case RunState.Terminated:
					theStartButton.Enabled = false;
					theStopButton.Enabled = false;
					break;
				case RunState.Started:
					theStartButton.Enabled = false;
					theStopButton.Enabled = true;
					break;
				case RunState.Stopped:
					theStartButton.Enabled = true;
					theStopButton.Enabled = false;
					break;
			}
		}

		private void theStartButton_Click(object sender, EventArgs e)
		{
			BeginInvoke(new MethodInvoker(ModuleContainer.Module.Start));
		}

		private void theStopButton_Click(object sender, EventArgs e)
		{
			BeginInvoke(new MethodInvoker(ModuleContainer.Module.Stop));
		}

		private void installAsWindowsServiceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				theServiceInstaller.InstallService();
				RefreshServiceMenuButtons();
				MessageBoxSuccess("Service installed successfully");
			}
			catch (Win32Exception wEx)
			{
				MessageBoxError(wEx);
			}
		}

		private void uninstallWindowsServiceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				theServiceInstaller.UninstallService();
				RefreshServiceMenuButtons();
				MessageBoxSuccess("Service uninstalled successfully");
			}
			catch (Win32Exception wEx)
			{
				MessageBoxError(wEx);
			}
		}

		private static void MessageBoxError(Win32Exception wEx)
		{
			MessageBox.Show(wEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private static void MessageBoxSuccess(string p)
		{
			MessageBox.Show(p, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}
