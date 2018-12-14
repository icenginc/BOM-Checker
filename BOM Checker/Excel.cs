using System;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Data;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;

namespace BOM_Checker
{
	public partial class Form1 : Form
	{
		private void export_to_excel() //requires EPPlus -> run in Nuget: "Install-Package EPPlus"
		{
			try
			{
				CommonOpenFileDialog dialog = new CommonOpenFileDialog(); //run in Nuget -> Install-Package Microsoft.WindowsAPICodePack-Shell -Version 1.1.0
				dialog.InitialDirectory = "\\\\backup-server\\Assembly Drawings\\";
				if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
				{
					excel_path = dialog.FileName;
				} //set path to variables
			}
			catch
			{
				MessageBox.Show("Error in Excel path select");
			}

			if (excel_path != null)
			{
				DataTable data1 = new DataTable();
				data1.Columns.AddRange(new DataColumn[] {
					new DataColumn("Name"), new DataColumn("Part Number"), new DataColumn("Error Message"),
					new DataColumn("EDIF Value"), new DataColumn("MRP Value"),
					new DataColumn("EDIF Aux"), new DataColumn("MRP Aux"),
					new DataColumn("EDIF Footprint"), new DataColumn("MRP Footprint"),
					new DataColumn("EDIF Package"), new DataColumn("MRP Package"), 
					new DataColumn("EDIF Quantity"), new DataColumn("MRP Quantity"),
					new DataColumn("EDIF Ref. Des."), new DataColumn("MRP Ref. Des.")
				}); //add columns of the data from mismatch datatype

				foreach (part_mismatch x in error_list)
				{
					data1.Rows.Add(x.name, x.partno.ToUpper(), x.error, x.edif_value, x.mrp_value, x.edif_aux, x.mrp_aux,
						x.edif_footprint, x.mrp_footprint, x.edif_package, x.mrp_package, x.edif_instances, x.mrp_instances,
						x.edif_instance_names, x.mrp_instance_names); //populate data table
				}

				using (var package = new ExcelPackage())
				{
					ExcelWorkbook workbook = package.Workbook;
					ExcelWorksheet worksheet = workbook.Worksheets.Add(("Data Comparison"));
					
					//----------------------------EDIF to PCMRP section-----------------------//
					var header = worksheet.Cells["A1:O1"]; //header columns
					header.Style.Font.Bold = true;  //make header bold
					header.LoadFromDataTable(data1, true); //add data after header
					//------------------------------------------------------------------------//

					var all_cells = worksheet.Cells[1, 1, worksheet.Dimension.End.Row, worksheet.Dimension.End.Column]; //all cells w/data
					all_cells.AutoFitColumns(); //auto fit width of all cells

					package.SaveAs(new System.IO.FileInfo(@excel_path)); //save to file
				}
			} //normal operation for saving excel file
			else
			{
				MessageBox.Show("Invalid Excel path select");
			}
		}
	}
}