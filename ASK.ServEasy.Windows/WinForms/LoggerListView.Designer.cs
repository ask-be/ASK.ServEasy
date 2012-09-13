namespace ASK.ServEasy.Windows.WinForms
{
	partial class LoggerListView
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));

			this.SuspendLayout();
			this.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
			this.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FullRowSelect = true;
			this.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.HideSelection = false;
			this.Location = new System.Drawing.Point(0, 25);
			this.MultiSelect = false;
			this.Name = "logListView";
			this.ShowItemToolTips = true;
			this.Size = new System.Drawing.Size(881, 324);
			this.TabIndex = 0;
			this.UseCompatibleStateImageBehavior = false;
			this.View = System.Windows.Forms.View.Details;

			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Time";
			this.columnHeader1.Width = 120;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Logger";
			this.columnHeader3.Width = 92;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Thread";
			this.columnHeader4.Width = 52;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Message";
			this.columnHeader5.Width = 540;

		}

		#endregion

		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
	}
}
