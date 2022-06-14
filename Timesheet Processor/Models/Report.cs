using System;

namespace Timesheet_Processor.Models
{
    public class Report
    {
        public int PayRunId { get; set; }
        public string EmployeeId { get; set; }
        public DateTime? StartTime { get; set; }
        public decimal SumOfValue { get; set; }
    }
}
