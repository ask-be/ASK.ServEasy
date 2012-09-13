namespace ASK.ServEasy.Windows.WinForms
{
	partial class LogEntriesDockContent
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
			this.theLoggerListView = new ASK.ServEasy.Windows.WinForms.LoggerListView();
			this.SuspendLayout();
			// 
			// theLoggerListView
			// 
			this.theLoggerListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.theLoggerListView.FullRowSelect = true;
			this.theLoggerListView.GridLines = true;
			this.theLoggerListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.theLoggerListView.HideSelection = false;
			this.theLoggerListView.Location = new System.Drawing.Point(0, 0);
			this.theLoggerListView.MultiSelect = false;
			this.theLoggerListView.Name = "theLoggerListView";
			this.theLoggerListView.ShowItemToolTips = true;
			this.theLoggerListView.Size = new System.Drawing.Size(864, 296);
			this.theLoggerListView.TabIndex = 0;
			this.theLoggerListView.UseCompatibleStateImageBehavior = false;
			this.theLoggerListView.View = System.Windows.Forms.View.Details;
			this.theLoggerListView.SelectedIndexChanged += new System.EventHandler(this.theLoggerListView_SelectedIndexChanged);
			// 
			// LogEntriesDockContent
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(864, 296);
			this.CloseButton = false;
			this.CloseButtonVisible = false;
			this.Controls.Add(this.theLoggerListView);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "LogEntriesDockContent";
			this.Text = "LogsDockContent";
			this.ResumeLayout(false);

		}

		#endregion

		private LoggerListView theLoggerListView;
	}
}