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
					excel_path = dialog.FileName + ".xlsx";
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
					new DataColumn("EDIF ModelNo"), new DataColumn("MRP ModelNo"),
					new DataColumn("EDIF Temp"), new DataColumn("MRP Temp"),
					new DataColumn("EDIF Qty"), new DataColumn("MRP Qty"),
					new DataColumn("EDIF Ref. Des."), new DataColumn("MRP Ref. Des.")
				}); //add columns of the data from mismatch datatype

				foreach (part_mismatch x in error_list)
				{
					string edif_instance_names = "", mrp_instance_names = "";

					foreach (string name in x.edif_instance_names)
						edif_instance_names += name + "; ";
					foreach (string name in x.mrp_instance_names)
						mrp_instance_names += name + "; ";

					data1.Rows.Add(x.name, x.partno.ToUpper(), x.error, x.edif_value, x.mrp_value, x.edif_aux, x.mrp_aux,
						x.edif_footprint, x.mrp_footprint, x.edif_package, x.mrp_package, x.edif_modelno, x.mrp_modelno,
						x.edif_temp, x.mrp_temp, x.edif_instances, x.mrp_instances, edif_instance_names, mrp_instance_names); //populate data table
				}

				using (var package = new ExcelPackage())
				{
					ExcelWorkbook workbook = package.Workbook;
					ExcelWorksheet worksheet = workbook.Worksheets.Add(("BOM " + bomno.ToUpper()));
					
					//----------------------------EDIF to PCMRP section-----------------------//
					var header = worksheet.Cells["A1:S1"]; //header columns
					header.Style.Font.Bold = true;  //make header bold
					header.LoadFromDataTable(data1, true); //add data after header
					//------------------------------------------------------------------------//

					var all_cells = worksheet.Cells[1, 1, worksheet.Dimension.End.Row, worksheet.Dimension.End.Column]; //all cells w/data
					all_cells.AutoFitColumns(); //auto fit width of all cells
					worksheet.View.FreezePanes(2, 1);

					for (int i = 2; i < worksheet.Dimension.End.Row; i++) //2 for skipping header
					{
						int offset = 0;
						string error = worksheet.Cells[i, 3].Text;
						string html_color = "#FAE8B6";

						if (error.Contains("Value"))
							offset = 2;
						else if (error.Contains("Aux"))
							offset = 3;
						else if (error.Contains("Footprint"))
							offset = 4;
						else if (error.Contains("Package"))
							offset = 5;
						else if (error.Contains("ModelNo"))
							offset = 6;
						else if (error.Contains("Temperature"))
							offset = 7;
						else if (error.Contains("Names"))
							offset = 9;
						else if (error.Contains("Instance"))
							offset = 8;
						

						
						if (error.Contains("missing"))
							html_color = "#C5DAF0";
						
						 
						var to_color = worksheet.Cells[i, offset * 2, i, (offset * 2) + 1];
						to_color.Style.Fill.PatternType = ExcelFillStyle.Solid;
						to_color.Style.Border.BorderAround(ExcelBorderStyle.Thin);
						to_color.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml(html_color));
					}

					try
					{
						package.SaveAs(new System.IO.FileInfo(@excel_path)); //save to file
					}
					catch (Exception e)
					{
						if(e is System.IO.IOException || e is System.InvalidOperationException)
							MessageBox.Show("Please close the Excel File before trying to overwrite.");
					}
				}
			} //normal operation for saving excel file
			else
			{
				MessageBox.Show("Invalid Excel path select");
			}
		}
	}
}