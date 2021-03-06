﻿namespace BOM_Checker
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
			this.button_parse = new System.Windows.Forms.Button();
			this.button_db = new System.Windows.Forms.Button();
			this.button_compare = new System.Windows.Forms.Button();
			this.textBox_status = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.button_edif_file = new System.Windows.Forms.Button();
			this.textbox_bomno = new System.Windows.Forms.TextBox();
			this.textbox_edif = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.button_edif_file2 = new System.Windows.Forms.Button();
			this.button_customer_excel = new System.Windows.Forms.Button();
			this.textbox_edif2 = new System.Windows.Forms.TextBox();
			this.textBox_excel = new System.Windows.Forms.TextBox();
			this.button_compare2 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.button5 = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.button_excel = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.panel3 = new System.Windows.Forms.Panel();
			this.button_output = new System.Windows.Forms.Button();
			this.button_clear = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkBox_instances = new System.Windows.Forms.CheckBox();
			this.checkBox_model = new System.Windows.Forms.CheckBox();
			this.checkBox_temp = new System.Windows.Forms.CheckBox();
			this.checkBox_package = new System.Windows.Forms.CheckBox();
			this.checkBox_value = new System.Windows.Forms.CheckBox();
			this.checkBox_footprint = new System.Windows.Forms.CheckBox();
			this.checkBox_auxw = new System.Windows.Forms.CheckBox();
			this.checkBox_auxv = new System.Windows.Forms.CheckBox();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.panel3.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(5, 8);
			this.label3.Name = "label3";
			this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.label3.Size = new System.Drawing.Size(63, 13);
			this.label3.TabIndex = 13;
			this.label3.Text = "Parameters:";
			// 
			// button_parse
			// 
			this.button_parse.Location = new System.Drawing.Point(232, 3);
			this.button_parse.Name = "button_parse";
			this.button_parse.Size = new System.Drawing.Size(69, 21);
			this.button_parse.TabIndex = 14;
			this.button_parse.Text = "Parse EDIF";
			this.button_parse.UseMnemonic = false;
			this.button_parse.UseVisualStyleBackColor = true;
			this.button_parse.Visible = false;
			this.button_parse.Click += new System.EventHandler(this.button_parse_Click);
			// 
			// button_db
			// 
			this.button_db.Location = new System.Drawing.Point(300, 3);
			this.button_db.Name = "button_db";
			this.button_db.Size = new System.Drawing.Size(62, 21);
			this.button_db.TabIndex = 15;
			this.button_db.Text = "Read DB";
			this.button_db.UseMnemonic = false;
			this.button_db.UseVisualStyleBackColor = true;
			this.button_db.Visible = false;
			this.button_db.Click += new System.EventHandler(this.button_db_Click);
			// 
			// button_compare
			// 
			this.button_compare.Location = new System.Drawing.Point(394, 29);
			this.button_compare.Name = "button_compare";
			this.button_compare.Size = new System.Drawing.Size(115, 39);
			this.button_compare.TabIndex = 16;
			this.button_compare.Text = "Compare EDIF/MRP";
			this.button_compare.UseMnemonic = false;
			this.button_compare.UseVisualStyleBackColor = true;
			this.button_compare.Click += new System.EventHandler(this.button_compare_Click);
			// 
			// textBox_status
			// 
			this.textBox_status.BackColor = System.Drawing.SystemColors.Window;
			this.textBox_status.Location = new System.Drawing.Point(12, 295);
			this.textBox_status.Multiline = true;
			this.textBox_status.Name = "textBox_status";
			this.textBox_status.ReadOnly = true;
			this.textBox_status.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox_status.Size = new System.Drawing.Size(389, 103);
			this.textBox_status.TabIndex = 17;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(14, 279);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(75, 13);
			this.label4.TabIndex = 18;
			this.label4.Text = "Status Output:";
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.button_edif_file);
			this.panel1.Controls.Add(this.textbox_bomno);
			this.panel1.Controls.Add(this.textbox_edif);
			this.panel1.Controls.Add(this.button_compare);
			this.panel1.Controls.Add(this.button_db);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.button_parse);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Location = new System.Drawing.Point(12, 191);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(534, 80);
			this.panel1.TabIndex = 19;
			// 
			// button_edif_file
			// 
			this.button_edif_file.Location = new System.Drawing.Point(347, 48);
			this.button_edif_file.Name = "button_edif_file";
			this.button_edif_file.Size = new System.Drawing.Size(24, 21);
			this.button_edif_file.TabIndex = 16;
			this.button_edif_file.Text = "...";
			this.button_edif_file.UseVisualStyleBackColor = true;
			this.button_edif_file.Click += new System.EventHandler(this.button_edif_file_Click);
			// 
			// textbox_bomno
			// 
			this.textbox_bomno.Location = new System.Drawing.Point(127, 28);
			this.textbox_bomno.Name = "textbox_bomno";
			this.textbox_bomno.Size = new System.Drawing.Size(73, 20);
			this.textbox_bomno.TabIndex = 15;
			this.textbox_bomno.TextChanged += new System.EventHandler(this.textbox_bomno_textchanged);
			// 
			// textbox_edif
			// 
			this.textbox_edif.Location = new System.Drawing.Point(127, 48);
			this.textbox_edif.Name = "textbox_edif";
			this.textbox_edif.Size = new System.Drawing.Size(215, 20);
			this.textbox_edif.TabIndex = 14;
			this.textbox_edif.TextChanged += new System.EventHandler(this.textbox_edif_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(5, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 13);
			this.label2.TabIndex = 13;
			this.label2.Text = "- BOM Number:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(5, 50);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 13);
			this.label1.TabIndex = 12;
			this.label1.Text = "- EDIF File:";
			// 
			// panel2
			// 
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.button_edif_file2);
			this.panel2.Controls.Add(this.button_customer_excel);
			this.panel2.Controls.Add(this.textbox_edif2);
			this.panel2.Controls.Add(this.textBox_excel);
			this.panel2.Controls.Add(this.button_compare2);
			this.panel2.Controls.Add(this.button4);
			this.panel2.Controls.Add(this.label5);
			this.panel2.Controls.Add(this.button5);
			this.panel2.Controls.Add(this.label6);
			this.panel2.Controls.Add(this.label7);
			this.panel2.Enabled = false;
			this.panel2.Location = new System.Drawing.Point(12, 86);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(534, 80);
			this.panel2.TabIndex = 20;
			// 
			// button_edif_file2
			// 
			this.button_edif_file2.Location = new System.Drawing.Point(347, 27);
			this.button_edif_file2.Name = "button_edif_file2";
			this.button_edif_file2.Size = new System.Drawing.Size(24, 21);
			this.button_edif_file2.TabIndex = 17;
			this.button_edif_file2.Text = "...";
			this.button_edif_file2.UseVisualStyleBackColor = true;
			this.button_edif_file2.Click += new System.EventHandler(this.button_edif_file2_Click);
			// 
			// button_customer_excel
			// 
			this.button_customer_excel.Location = new System.Drawing.Point(347, 48);
			this.button_customer_excel.Name = "button_customer_excel";
			this.button_customer_excel.Size = new System.Drawing.Size(24, 21);
			this.button_customer_excel.TabIndex = 16;
			this.button_customer_excel.Text = "...";
			this.button_customer_excel.UseVisualStyleBackColor = true;
			this.button_customer_excel.Click += new System.EventHandler(this.button_customer_excel_Click);
			// 
			// textbox_edif2
			// 
			this.textbox_edif2.Location = new System.Drawing.Point(127, 28);
			this.textbox_edif2.Name = "textbox_edif2";
			this.textbox_edif2.Size = new System.Drawing.Size(215, 20);
			this.textbox_edif2.TabIndex = 15;
			this.textbox_edif2.TextChanged += new System.EventHandler(this.textbox_edif2_TextChanged);
			// 
			// textBox_excel
			// 
			this.textBox_excel.Location = new System.Drawing.Point(127, 48);
			this.textBox_excel.Name = "textBox_excel";
			this.textBox_excel.Size = new System.Drawing.Size(215, 20);
			this.textBox_excel.TabIndex = 14;
			this.textBox_excel.TextChanged += new System.EventHandler(this.textBox_excel_TextChanged);
			// 
			// button_compare2
			// 
			this.button_compare2.Location = new System.Drawing.Point(394, 30);
			this.button_compare2.Name = "button_compare2";
			this.button_compare2.Size = new System.Drawing.Size(115, 38);
			this.button_compare2.TabIndex = 16;
			this.button_compare2.Text = "Compare Excel/EDIF";
			this.button_compare2.UseMnemonic = false;
			this.button_compare2.UseVisualStyleBackColor = true;
			this.button_compare2.Click += new System.EventHandler(this.button_compare2_Click);
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(300, 1);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(62, 21);
			this.button4.TabIndex = 15;
			this.button4.Text = "Read DB";
			this.button4.UseMnemonic = false;
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Visible = false;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(5, 30);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(59, 13);
			this.label5.TabIndex = 13;
			this.label5.Text = "- EDIF File:";
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(232, 1);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(69, 21);
			this.button5.TabIndex = 14;
			this.button5.Text = "Parse EDIF";
			this.button5.UseMnemonic = false;
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Visible = false;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(5, 50);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(108, 13);
			this.label6.TabIndex = 12;
			this.label6.Text = "- Customer Excel File:";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(5, 8);
			this.label7.Name = "label7";
			this.label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.label7.Size = new System.Drawing.Size(63, 13);
			this.label7.TabIndex = 13;
			this.label7.Text = "Parameters:";
			// 
			// button_excel
			// 
			this.button_excel.Enabled = false;
			this.button_excel.Location = new System.Drawing.Point(12, 7);
			this.button_excel.Name = "button_excel";
			this.button_excel.Size = new System.Drawing.Size(115, 32);
			this.button_excel.TabIndex = 21;
			this.button_excel.Text = "Export to Excel";
			this.button_excel.UseMnemonic = false;
			this.button_excel.UseVisualStyleBackColor = true;
			this.button_excel.Click += new System.EventHandler(this.button_excel_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pictureBox1.Image = global::BOM_Checker.Properties.Resources.ICE_LOGO;
			this.pictureBox1.Location = new System.Drawing.Point(12, 9);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(201, 57);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 22;
			this.pictureBox1.TabStop = false;
			// 
			// panel3
			// 
			this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel3.Controls.Add(this.button_output);
			this.panel3.Controls.Add(this.button_clear);
			this.panel3.Controls.Add(this.button_excel);
			this.panel3.Location = new System.Drawing.Point(407, 295);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(139, 103);
			this.panel3.TabIndex = 23;
			// 
			// button_output
			// 
			this.button_output.Location = new System.Drawing.Point(12, 41);
			this.button_output.Name = "button_output";
			this.button_output.Size = new System.Drawing.Size(115, 32);
			this.button_output.TabIndex = 23;
			this.button_output.Text = "Expand Output";
			this.button_output.UseMnemonic = false;
			this.button_output.UseVisualStyleBackColor = true;
			this.button_output.Click += new System.EventHandler(this.button_output_Click);
			// 
			// button_clear
			// 
			this.button_clear.Location = new System.Drawing.Point(12, 75);
			this.button_clear.Name = "button_clear";
			this.button_clear.Size = new System.Drawing.Size(115, 21);
			this.button_clear.TabIndex = 22;
			this.button_clear.Text = "Clear";
			this.button_clear.UseMnemonic = false;
			this.button_clear.UseVisualStyleBackColor = true;
			this.button_clear.Click += new System.EventHandler(this.button_clear_Click);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(10, 177);
			this.label8.Name = "label8";
			this.label8.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.label8.Size = new System.Drawing.Size(221, 13);
			this.label8.TabIndex = 24;
			this.label8.Text = "Schematic(EDIF) to BOM Database Checker:";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(9, 72);
			this.label9.Name = "label9";
			this.label9.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.label9.Size = new System.Drawing.Size(221, 13);
			this.label9.TabIndex = 25;
			this.label9.Text = "Customer Excel to Schematic(EDIF) Checker:";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.checkBox_auxv);
			this.groupBox1.Controls.Add(this.checkBox_instances);
			this.groupBox1.Controls.Add(this.checkBox_model);
			this.groupBox1.Controls.Add(this.checkBox_temp);
			this.groupBox1.Controls.Add(this.checkBox_package);
			this.groupBox1.Controls.Add(this.checkBox_value);
			this.groupBox1.Controls.Add(this.checkBox_footprint);
			this.groupBox1.Controls.Add(this.checkBox_auxw);
			this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.groupBox1.Location = new System.Drawing.Point(219, 9);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(327, 57);
			this.groupBox1.TabIndex = 26;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Comparison Parameters";
			// 
			// checkBox_instances
			// 
			this.checkBox_instances.AutoSize = true;
			this.checkBox_instances.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
			this.checkBox_instances.Checked = true;
			this.checkBox_instances.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox_instances.Location = new System.Drawing.Point(274, 20);
			this.checkBox_instances.Name = "checkBox_instances";
			this.checkBox_instances.Size = new System.Drawing.Size(52, 31);
			this.checkBox_instances.TabIndex = 6;
			this.checkBox_instances.Text = "Instance";
			this.checkBox_instances.UseVisualStyleBackColor = true;
			// 
			// checkBox_model
			// 
			this.checkBox_model.AutoSize = true;
			this.checkBox_model.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
			this.checkBox_model.Checked = true;
			this.checkBox_model.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox_model.Location = new System.Drawing.Point(240, 20);
			this.checkBox_model.Name = "checkBox_model";
			this.checkBox_model.Size = new System.Drawing.Size(40, 31);
			this.checkBox_model.TabIndex = 5;
			this.checkBox_model.Text = "Model";
			this.checkBox_model.UseVisualStyleBackColor = true;
			// 
			// checkBox_temp
			// 
			this.checkBox_temp.AutoSize = true;
			this.checkBox_temp.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
			this.checkBox_temp.Checked = true;
			this.checkBox_temp.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox_temp.Location = new System.Drawing.Point(204, 20);
			this.checkBox_temp.Name = "checkBox_temp";
			this.checkBox_temp.Size = new System.Drawing.Size(41, 31);
			this.checkBox_temp.TabIndex = 4;
			this.checkBox_temp.Text = "Temp ";
			this.checkBox_temp.UseVisualStyleBackColor = true;
			// 
			// checkBox_package
			// 
			this.checkBox_package.AutoSize = true;
			this.checkBox_package.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
			this.checkBox_package.Checked = true;
			this.checkBox_package.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox_package.Location = new System.Drawing.Point(157, 20);
			this.checkBox_package.Name = "checkBox_package";
			this.checkBox_package.Size = new System.Drawing.Size(54, 31);
			this.checkBox_package.TabIndex = 3;
			this.checkBox_package.Text = "Package";
			this.checkBox_package.UseVisualStyleBackColor = true;
			// 
			// checkBox_value
			// 
			this.checkBox_value.AutoSize = true;
			this.checkBox_value.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
			this.checkBox_value.Checked = true;
			this.checkBox_value.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox_value.Location = new System.Drawing.Point(125, 20);
			this.checkBox_value.Name = "checkBox_value";
			this.checkBox_value.Size = new System.Drawing.Size(38, 31);
			this.checkBox_value.TabIndex = 2;
			this.checkBox_value.Text = "Value";
			this.checkBox_value.UseVisualStyleBackColor = true;
			// 
			// checkBox_footprint
			// 
			this.checkBox_footprint.AutoSize = true;
			this.checkBox_footprint.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
			this.checkBox_footprint.Checked = true;
			this.checkBox_footprint.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox_footprint.Location = new System.Drawing.Point(80, 20);
			this.checkBox_footprint.Name = "checkBox_footprint";
			this.checkBox_footprint.Size = new System.Drawing.Size(52, 31);
			this.checkBox_footprint.TabIndex = 1;
			this.checkBox_footprint.Text = "Footprint";
			this.checkBox_footprint.UseVisualStyleBackColor = true;
			// 
			// checkBox_auxw
			// 
			this.checkBox_auxw.AutoSize = true;
			this.checkBox_auxw.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
			this.checkBox_auxw.Checked = true;
			this.checkBox_auxw.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox_auxw.Location = new System.Drawing.Point(40, 20);
			this.checkBox_auxw.Name = "checkBox_auxw";
			this.checkBox_auxw.Size = new System.Drawing.Size(46, 31);
			this.checkBox_auxw.TabIndex = 0;
			this.checkBox_auxw.Text = "Aux(W)";
			this.checkBox_auxw.UseVisualStyleBackColor = true;
			// 
			// checkBox_auxv
			// 
			this.checkBox_auxv.AutoSize = true;
			this.checkBox_auxv.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
			this.checkBox_auxv.Checked = true;
			this.checkBox_auxv.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox_auxv.Location = new System.Drawing.Point(2, 20);
			this.checkBox_auxv.Name = "checkBox_auxv";
			this.checkBox_auxv.Size = new System.Drawing.Size(42, 31);
			this.checkBox_auxv.TabIndex = 7;
			this.checkBox_auxv.Text = "Aux(V)";
			this.checkBox_auxv.UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(562, 409);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBox_status);
			this.Name = "Form1";
			this.Text = "BOM Checker v1.0";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.panel3.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button button_parse;
		private System.Windows.Forms.Button button_db;
		private System.Windows.Forms.Button button_compare;
		private System.Windows.Forms.TextBox textBox_status;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button button_edif_file;
		private System.Windows.Forms.TextBox textbox_bomno;
		private System.Windows.Forms.TextBox textbox_edif;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button button_edif_file2;
		private System.Windows.Forms.Button button_customer_excel;
		private System.Windows.Forms.TextBox textbox_edif2;
		private System.Windows.Forms.TextBox textBox_excel;
		private System.Windows.Forms.Button button_compare2;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button button_excel;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button button_clear;
		private System.Windows.Forms.Button button_output;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkBox_auxw;
		private System.Windows.Forms.CheckBox checkBox_footprint;
		private System.Windows.Forms.CheckBox checkBox_instances;
		private System.Windows.Forms.CheckBox checkBox_model;
		private System.Windows.Forms.CheckBox checkBox_temp;
		private System.Windows.Forms.CheckBox checkBox_package;
		private System.Windows.Forms.CheckBox checkBox_value;
		private System.Windows.Forms.CheckBox checkBox_auxv;
	}
}

