namespace ASK.ServEasy.Windows.WinForms
{
	partial class ThreadsDockContent
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
			this.theTreeViewAdv = new Aga.Controls.Tree.TreeViewAdv();
			this.theNameTreeColumn = new Aga.Controls.Tree.TreeColumn();
			this.theStateTreeColumn = new Aga.Controls.Tree.TreeColumn();
			this.theStatusMessageTreeColumn = new Aga.Controls.Tree.TreeColumn();
			this.theNextRunTreeColumn = new Aga.Controls.Tree.TreeColumn();
			this.theScheduleTreeColumn = new Aga.Controls.Tree.TreeColumn();
			this.theNameNodeTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
			this.theNextRunNodeTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
			this.theScheduleNodeTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
			this.theStatusMessageNodeTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
			this.theStateNodeTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
			this.SuspendLayout();
			// 
			// theTreeViewAdv
			// 
			this.theTreeViewAdv.BackColor = System.Drawing.SystemColors.Window;
			this.theTreeViewAdv.Columns.Add(this.theNameTreeColumn);
			this.theTreeViewAdv.Columns.Add(this.theStateTreeColumn);
			this.theTreeViewAdv.Columns.Add(this.theStatusMessageTreeColumn);
			this.theTreeViewAdv.Columns.Add(this.theNextRunTreeColumn);
			this.theTreeViewAdv.Columns.Add(this.theScheduleTreeColumn);
			this.theTreeViewAdv.DefaultToolTipProvider = null;
			this.theTreeViewAdv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.theTreeViewAdv.DragDropMarkColor = System.Drawing.Color.Black;
			this.theTreeViewAdv.LineColor = System.Drawing.SystemColors.ControlDark;
			this.theTreeViewAdv.Location = new System.Drawing.Point(0, 0);
			this.theTreeViewAdv.Model = null;
			this.theTreeViewAdv.Name = "theTreeViewAdv";
			this.theTreeViewAdv.NodeControls.Add(this.theNameNodeTextBox);
			this.theTreeViewAdv.NodeControls.Add(this.theNextRunNodeTextBox);
			this.theTreeViewAdv.NodeControls.Add(this.theScheduleNodeTextBox);
			this.theTreeViewAdv.NodeControls.Add(this.theStatusMessageNodeTextBox);
			this.theTreeViewAdv.NodeControls.Add(this.theStateNodeTextBox);
			this.theTreeViewAdv.SelectedNode = null;
			this.theTreeViewAdv.Size = new System.Drawing.Size(509, 270);
			this.theTreeViewAdv.TabIndex = 0;
			this.theTreeViewAdv.Text = "treeViewAdv1";
			this.theTreeViewAdv.UseColumns = true;
			// 
			// theNameTreeColumn
			// 
			this.theNameTreeColumn.Header = "Name";
			this.theNameTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
			this.theNameTreeColumn.Tag = null;
			this.theNameTreeColumn.TooltipText = null;
			this.theNameTreeColumn.Width = 150;
			// 
			// theStateTreeColumn
			// 
			this.theStateTreeColumn.Header = "State";
			this.theStateTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
			this.theStateTreeColumn.Tag = null;
			this.theStateTreeColumn.TooltipText = null;
			// 
			// theStatusMessageTreeColumn
			// 
			this.theStatusMessageTreeColumn.Header = "Status Message";
			this.theStatusMessageTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
			this.theStatusMessageTreeColumn.Tag = null;
			this.theStatusMessageTreeColumn.TooltipText = null;
			this.theStatusMessageTreeColumn.Width = 150;
			// 
			// theNextRunTreeColumn
			// 
			this.theNextRunTreeColumn.Header = "NextRun";
			this.theNextRunTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
			this.theNextRunTreeColumn.Tag = null;
			this.theNextRunTreeColumn.TooltipText = null;
			this.theNextRunTreeColumn.Width = 80;
			// 
			// theScheduleTreeColumn
			// 
			this.theScheduleTreeColumn.Header = "Schedule";
			this.theScheduleTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
			this.theScheduleTreeColumn.Tag = null;
			this.theScheduleTreeColumn.TooltipText = null;
			this.theScheduleTreeColumn.Width = 70;
			// 
			// theNameNodeTextBox
			// 
			this.theNameNodeTextBox.DataPropertyName = "Name";
			this.theNameNodeTextBox.IncrementalSearchEnabled = true;
			this.theNameNodeTextBox.LeftMargin = 3;
			this.theNameNodeTextBox.ParentColumn = this.theNameTreeColumn;
			// 
			// theNextRunNodeTextBox
			// 
			this.theNextRunNodeTextBox.DataPropertyName = "NextRun";
			this.theNextRunNodeTextBox.IncrementalSearchEnabled = true;
			this.theNextRunNodeTextBox.LeftMargin = 3;
			this.theNextRunNodeTextBox.ParentColumn = this.theNextRunTreeColumn;
			// 
			// theScheduleNodeTextBox
			// 
			this.theScheduleNodeTextBox.DataPropertyName = "Schedule";
			this.theScheduleNodeTextBox.IncrementalSearchEnabled = true;
			this.theScheduleNodeTextBox.LeftMargin = 3;
			this.theScheduleNodeTextBox.ParentColumn = this.theScheduleTreeColumn;
			// 
			// theStatusMessageNodeTextBox
			// 
			this.theStatusMessageNodeTextBox.DataPropertyName = "StatusMessage";
			this.theStatusMessageNodeTextBox.IncrementalSearchEnabled = true;
			this.theStatusMessageNodeTextBox.LeftMargin = 3;
			this.theStatusMessageNodeTextBox.ParentColumn = this.theStatusMessageTreeColumn;
			this.theStatusMessageNodeTextBox.Trimming = System.Drawing.StringTrimming.EllipsisCharacter;
			// 
			// theStateNodeTextBox
			// 
			this.theStateNodeTextBox.DataPropertyName = "State";
			this.theStateNodeTextBox.IncrementalSearchEnabled = true;
			this.theStateNodeTextBox.LeftMargin = 3;
			this.theStateNodeTextBox.ParentColumn = this.theStateTreeColumn;
			// 
			// ThreadsDockContent
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(509, 270);
			this.CloseButton = false;
			this.CloseButtonVisible = false;
			this.Controls.Add(this.theTreeViewAdv);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "ThreadsDockContent";
			this.Load += new System.EventHandler(this.ThreadsDockContent_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private Aga.Controls.Tree.TreeViewAdv theTreeViewAdv;
		private Aga.Controls.Tree.TreeColumn theNameTreeColumn;
		private Aga.Controls.Tree.TreeColumn theStateTreeColumn;
		private Aga.Controls.Tree.TreeColumn theStatusMessageTreeColumn;
		private Aga.Controls.Tree.TreeColumn theNextRunTreeColumn;
		private Aga.Controls.Tree.TreeColumn theScheduleTreeColumn;
		private Aga.Controls.Tree.NodeControls.NodeTextBox theNameNodeTextBox;
		private Aga.Controls.Tree.NodeControls.NodeTextBox theNextRunNodeTextBox;
		private Aga.Controls.Tree.NodeControls.NodeTextBox theScheduleNodeTextBox;
		private Aga.Controls.Tree.NodeControls.NodeTextBox theStatusMessageNodeTextBox;
		private Aga.Controls.Tree.NodeControls.NodeTextBox theStateNodeTextBox;

	}
}
