using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Timesheet_Processor.Models;

namespace Timesheet_Processor
{
    public class ReportManager
    {
        public void GenerateReport(IList<Timesheet> list)
        {
            List<Report> processData = PreprocessData(list);
            string filePath = Directory.GetCurrentDirectory();
            string timePrefix = DateTime.Now.ToString("yyyyMMdd");
            string fileWithPath = $"{filePath}\\Timesheet-{timePrefix}.csv";

            // generate csv file
            using (var writer = new StreamWriter(fileWithPath))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(processData);
                }
            }

            // print generated file path
            Console.WriteLine("\nPlease find the generated file here -> ");
            Console.WriteLine($"{fileWithPath.Replace(@"\\", @"\")}");
        }

        private List<Report> PreprocessData(IList<Timesheet> list)
        {
            List<Report> reportData = new List<Report>();

            foreach (var item in list)
            {
                IList<Report> tempReportData = item.Values.GroupBy(x => new { x.EmployeeId, x.StartDate })
                                                .Select(s => new Report
                                                {
                                                    PayRunId = item.PayRunId,
                                                    EmployeeId = s.FirstOrDefault().EmployeeId,
                                                    StartTime = s.FirstOrDefault().StartDate,
                                                    SumOfValue = s.Sum(y => y.Value)
                                                }).ToList();

                reportData.AddRange(tempReportData);
            }

            return reportData;
        }
    }
}
