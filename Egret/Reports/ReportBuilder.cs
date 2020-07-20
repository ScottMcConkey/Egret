using ClosedXML.Excel;
using Egret.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Egret.Reports
{
    public class ReportBuilder
    {
        public Stream Build(IReport report)
        {
            var stream = new MemoryStream();
            var rowNumber = 1;

            using (XLWorkbook workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(report.Title);
                var dateColumn = 10;

                // Header
                worksheet.Cell(rowNumber, 1).Value = report.Title;
                worksheet.FirstColumn().Width = report.Title.Length;
                worksheet.Column(10).Width = dateColumn;
                rowNumber++;
                worksheet.Cell(rowNumber, 1).Value = report.ImagePath;
                worksheet.Cell(rowNumber, dateColumn).Value = report.ReportDate;
                rowNumber++;
                
                for (var row = 0; row < report.Details.Count; row++)
                {
                    var propertyCount = report.Details[row].GetType().GetProperties().Count();
                    rowNumber++;

                    for (var prop = 0; prop < propertyCount; prop++)
                    {
                        worksheet.Cell(rowNumber, prop + 1).Value = 
                            (report.Details[row]).GetType().GetProperties()[prop].GetValue(report.Details[row]).ToString();
                    }

                    //worksheet.Cell(row + 3, 1).Value = (report.Details[row]).GetType().GetProperties()[0].GetValue(report.Details[row]).ToString();//columns[row].Name;
                    //worksheet.Cell(row + 3, 2).Value = report.Details[row].ToString();//columns[row].StockValue.ToString();
                }
                workbook.SaveAs(stream);
            }

            stream.Position = 0;

            return stream; // File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Report.xlsx");
        }
    }
}
