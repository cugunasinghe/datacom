using System.Collections.Generic;
using System.Linq;
using Timesheet_Processor.Models;

namespace Timesheet_Processor
{
    public class ReportManager
    {
        public ReportManager()
        {

        }

        public void GenerateReport(IList<Timesheet> list)
        {
            List<Report> processData = PreprocessData(list);
            // TODO
        }

        private List<Report> PreprocessData(IList<Timesheet> list)
        {
            List<Report> reportData = new List<Report>();

            foreach(var item in list)
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
