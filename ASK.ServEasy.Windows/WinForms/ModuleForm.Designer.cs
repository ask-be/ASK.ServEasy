namespace ASK.ServEasy.Windows.WinForms
{
	partial class ModuleForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.theStopButton = new System.Windows.Forms.Button();
			this.theStartButton = new System.Windows.Forms.Button();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.installAsWindowsServiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.uninstallWindowsServiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.theLogTimer = new System.Windows.Forms.Timer(this.components);
			this.theDockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
			this.groupBox1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.theStopButton);
			this.groupBox1.Controls.Add(this.theStartButton);
			this.groupBox1.Location = new System.Drawing.Point(1, 480);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
			this.groupBox1.Size = new System.Drawing.Size(997, 60);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			// 
			// theStopButton
			// 
			this.theStopButton.Location = new System.Drawing.Point(116, 20);
			this.theStopButton.Margin = new System.Windows.Forms.Padding(4);
			this.theStopButton.Name = "theStopButton";
			this.theStopButton.Size = new System.Drawing.Size(100, 28);
			this.theStopButton.TabIndex = 1;
			this.theStopButton.Text = "Stop";
			this.theStopButton.UseVisualStyleBackColor = true;
			this.theStopButton.Click += new System.EventHandler(this.theStopButton_Click);
			// 
			// theStartButton
			// 
			this.theStartButton.Location = new System.Drawing.Point(8, 20);
			this.theStartButton.Margin = new System.Windows.Forms.Padding(4);
			this.theStartButton.Name = "theStartButton";
			this.theStartButton.Size = new System.Drawing.Size(100, 28);
			this.theStartButton.TabIndex = 0;
			this.theStartButton.Text = "Start";
			this.theStartButton.UseVisualStyleBackColor = true;
			this.theStartButton.Click += new System.EventHandler(this.theStartButton_Click);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.fileToolStripMenuItem, this.toolsToolStripMenuItem });
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
			this.menuStrip1.Size = new System.Drawing.Size(1001, 28);
			this.menuStrip1.TabIndex = 3;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.closeToolStripMenuItem });
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(114, 24);
			this.closeToolStripMenuItem.Text = "Close";
			// 
			// toolsToolStripMenuItem
			// 
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.installAsWindowsServiceToolStripMenuItem, this.uninstallWindowsServiceToolStripMenuItem });
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new System.Drawing.Size(56, 24);
			this.toolsToolStripMenuItem.Text = "Tools";
			// 
			// installAsWindowsServiceToolStripMenuItem
			// 
			this.installAsWindowsServiceToolStripMenuItem.Name = "installAsWindowsServiceToolStripMenuItem";
			this.installAsWindowsServiceToolStripMenuItem.Size = new System.Drawing.Size(251, 24);
			this.installAsWindowsServiceToolStripMenuItem.Text = "Install Windows Service";
			this.installAsWindowsServiceToolStripMenuItem.Click += new System.EventHandler(this.installAsWindowsServiceToolStripMenuItem_Click);
			// 
			// uninstallWindowsServiceToolStripMenuItem
			// 
			this.uninstallWindowsServiceToolStripMenuItem.Name = "uninstallWindowsServiceToolStripMenuItem";
			this.uninstallWindowsServiceToolStripMenuItem.Size = new System.Drawing.Size(251, 24);
			this.uninstallWindowsServiceToolStripMenuItem.Text = "Uninstall Windows Service";
			this.uninstallWindowsServiceToolStripMenuItem.Click += new System.EventHandler(this.uninstallWindowsServiceToolStripMenuItem_Click);
			// 
			// theLogTimer
			// 
			this.theLogTimer.Enabled = true;
			this.theLogTimer.Interval = 250;
			this.theLogTimer.Tick += new System.EventHandler(this.theLogTimer_Tick);
			// 
			// theDockPanel
			// 
			this.theDockPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
			this.theDockPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.theDockPanel.DockBackColor = System.Drawing.SystemColors.Control;
			this.theDockPanel.DockBottomPortion = 0.5D;
			this.theDockPanel.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingSdi;
			this.theDockPanel.Location = new System.Drawing.Point(1, 33);
			this.theDockPanel.Margin = new System.Windows.Forms.Padding(4);
			this.theDockPanel.Name = "theDockPanel";
			this.theDockPanel.Size = new System.Drawing.Size(997, 439);
			this.theDockPanel.TabIndex = 1;
			// 
			// ModuleForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1001, 542);
			this.Controls.Add(this.theDockPanel);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "ModuleForm";
			this.Text = "ModuleForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModuleForm_FormClosing);
			this.Load += new System.EventHandler(this.ModuleForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button theStopButton;
		private System.Windows.Forms.Button theStartButton;
		private WeifenLuo.WinFormsUI.Docking.DockPanel theDockPanel;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
		private System.Windows.Forms.Timer theLogTimer;
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem installAsWindowsServiceToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem uninstallWindowsServiceToolStripMenuItem;
	}
}