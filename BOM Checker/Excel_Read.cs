using System;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Data;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using System.IO;

namespace BOM_Checker
{
	public partial class Form1 : Form
	{
		private DataTable read_excel_file(bool hasHeader = true)
		{
			using (var pck = new OfficeOpenXml.ExcelPackage())
			{
				using (var stream = File.OpenRead(excel_path))
				{
					pck.Load(stream);
				}
				var ws = pck.Workbook.Worksheets[0];
				DataTable tbl = new DataTable();
				foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
				{
					tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
				}
				var startRow = hasHeader ? 2 : 1;
				for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
				{
					var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
					DataRow row = tbl.Rows.Add();
					foreach (var cell in wsRow)
					{
						row[cell.Start.Column - 1] = cell.Text;
					}
				}
				return tbl;
			} //sourced online -> https://stackoverflow.com/questions/13396604/excel-to-datatable-using-epplus-excel-locked-for-editing
		}
	}
}