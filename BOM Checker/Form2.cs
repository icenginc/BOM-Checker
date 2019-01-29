using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BOM_Checker
{
	public partial class Form2 : Form
	{
		public Form2(string contents)
		{
			InitializeComponent();
			textBox_contents.Font = new Font(new FontFamily("Microsoft Sans Serif"), 11, FontStyle.Regular);
			textBox_contents.Text = contents;
			textBox_contents.Select(textBox_contents.Text.Length, 1);
			textBox_contents.ScrollToCaret();
		}
	}
}
