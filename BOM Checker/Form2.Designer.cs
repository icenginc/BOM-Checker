namespace BOM_Checker
{
	partial class Form2
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
			this.textBox_contents = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// textBox_contents
			// 
			this.textBox_contents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox_contents.BackColor = System.Drawing.SystemColors.Window;
			this.textBox_contents.Location = new System.Drawing.Point(12, 12);
			this.textBox_contents.Multiline = true;
			this.textBox_contents.Name = "textBox_contents";
			this.textBox_contents.ReadOnly = true;
			this.textBox_contents.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox_contents.Size = new System.Drawing.Size(718, 663);
			this.textBox_contents.TabIndex = 0;
			// 
			// Form2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(742, 687);
			this.Controls.Add(this.textBox_contents);
			this.Name = "Form2";
			this.Text = "Output Viewer";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBox_contents;
	}
}