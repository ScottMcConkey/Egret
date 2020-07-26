using ClosedXML.Excel;
using System.IO;
using System.Linq;

namespace Egret.Reports
{
    public class ReportBuilder
    {
        public Stream Build(Report report)
        {
            var stream = new MemoryStream();
            var rowNumber = 1;
            var columnNumber = 1;


            using (XLWorkbook workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(report.WorksheetName);

                worksheet.FirstRow().Height = 22;
                worksheet.FirstColumn().Width = report.Title.Length;
                worksheet.Column(2).Style.NumberFormat.Format = "#,##0.00";

                // Title
                worksheet.Cell(rowNumber, 1).Value = report.Title;
                worksheet.Cell(rowNumber, 1).Style.Font.Bold = true;
                worksheet.Cell(rowNumber, 1).Style.Font.FontSize = 18;

                rowNumber++;

                // Date
                worksheet.Cell(rowNumber, 1).Value = report.ReportDate;
                worksheet.Cell(rowNumber, 1).Style.NumberFormat.Format = "mm/dd/yyyy";
                worksheet.Cell(rowNumber, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                rowNumber++;
                rowNumber++;

                // Column Titles
                foreach (string title in report.ColumnNames.Split(','))
                {
                    worksheet.Cell(rowNumber, columnNumber).Value = title;
                    worksheet.Cell(rowNumber, columnNumber).Style.Font.Bold = true;
                    columnNumber++;
                }

                // Body
                for (var row = 0; row < report.Details.Count; row++)
                {
                    var propertyCount = report.Details[row].GetType().GetProperties().Count();

                    rowNumber++;

                    for (var prop = 0; prop < propertyCount; prop++)
                    {
                        worksheet.Cell(rowNumber, prop + 1).Value =
                            (report.Details[row]).GetType().GetProperties()[prop].GetValue(report.Details[row]).ToString();
                    }
                }

                workbook.SaveAs(stream);
            }

            stream.Position = 0;

            return stream;
        }
    }
}
