namespace BOM_Checker
{
	partial class Form1
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
			this.label3 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.textbox_temp_reports = new System.Windows.Forms.TextBox();
			this.textbox_edif = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.button_parse = new System.Windows.Forms.Button();
			this.button_db = new System.Windows.Forms.Button();
			this.button_compare = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(30, 169);
			this.label3.Name = "label3";
			this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.label3.Size = new System.Drawing.Size(75, 13);
			this.label3.TabIndex = 13;
			this.label3.Text = "File Locations:";
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(379, 209);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(24, 21);
			this.button2.TabIndex = 12;
			this.button2.Text = "...";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(379, 189);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(24, 21);
			this.button1.TabIndex = 11;
			this.button1.Text = "...";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// textbox_temp_reports
			// 
			this.textbox_temp_reports.Location = new System.Drawing.Point(159, 209);
			this.textbox_temp_reports.Name = "textbox_temp_reports";
			this.textbox_temp_reports.Size = new System.Drawing.Size(215, 20);
			this.textbox_temp_reports.TabIndex = 10;
			// 
			// textbox_edif
			// 
			this.textbox_edif.Location = new System.Drawing.Point(159, 190);
			this.textbox_edif.Name = "textbox_edif";
			this.textbox_edif.Size = new System.Drawing.Size(215, 20);
			this.textbox_edif.TabIndex = 9;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(35, 209);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(102, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "- Temp Report Files:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(35, 190);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "- EDIF File:";
			// 
			// button_parse
			// 
			this.button_parse.Location = new System.Drawing.Point(439, 190);
			this.button_parse.Name = "button_parse";
			this.button_parse.Size = new System.Drawing.Size(91, 21);
			this.button_parse.TabIndex = 14;
			this.button_parse.Text = "Parse EDIF";
			this.button_parse.UseMnemonic = false;
			this.button_parse.UseVisualStyleBackColor = true;
			this.button_parse.Click += new System.EventHandler(this.button_parse_Click);
			// 
			// button_db
			// 
			this.button_db.Location = new System.Drawing.Point(439, 210);
			this.button_db.Name = "button_db";
			this.button_db.Size = new System.Drawing.Size(91, 21);
			this.button_db.TabIndex = 15;
			this.button_db.Text = "Read DB";
			this.button_db.UseMnemonic = false;
			this.button_db.UseVisualStyleBackColor = true;
			this.button_db.Click += new System.EventHandler(this.button_db_Click);
			// 
			// button_compare
			// 
			this.button_compare.Location = new System.Drawing.Point(439, 230);
			this.button_compare.Name = "button_compare";
			this.button_compare.Size = new System.Drawing.Size(91, 21);
			this.button_compare.TabIndex = 16;
			this.button_compare.Text = "Compare";
			this.button_compare.UseMnemonic = false;
			this.button_compare.UseVisualStyleBackColor = true;
			this.button_compare.Click += new System.EventHandler(this.button_compare_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(580, 261);
			this.Controls.Add(this.button_compare);
			this.Controls.Add(this.button_db);
			this.Controls.Add(this.button_parse);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textbox_temp_reports);
			this.Controls.Add(this.textbox_edif);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox textbox_temp_reports;
		private System.Windows.Forms.TextBox textbox_edif;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button_parse;
		private System.Windows.Forms.Button button_db;
		private System.Windows.Forms.Button button_compare;
	}
}

