namespace ASK.ServEasy.Windows.WinForms
{
	partial class LogEntryDetailsDockContent
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
			this.theDetailsTextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// theDetailsTextBox
			// 
			this.theDetailsTextBox.AcceptsReturn = true;
			this.theDetailsTextBox.AcceptsTab = true;
			this.theDetailsTextBox.BackColor = System.Drawing.SystemColors.Window;
			this.theDetailsTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.theDetailsTextBox.Location = new System.Drawing.Point(0, 0);
			this.theDetailsTextBox.Multiline = true;
			this.theDetailsTextBox.Name = "theDetailsTextBox";
			this.theDetailsTextBox.ReadOnly = true;
			this.theDetailsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.theDetailsTextBox.Size = new System.Drawing.Size(733, 262);
			this.theDetailsTextBox.TabIndex = 0;
			// 
			// LogEntryDetailsDockContent
			// 
			this.AutoHidePortion = 0.5D;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(733, 262);
			this.Controls.Add(this.theDetailsTextBox);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "LogEntryDetailsDockContent";
			this.TabText = "Details";
			this.Text = "Details";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		internal System.Windows.Forms.TextBox theDetailsTextBox;

	}
}