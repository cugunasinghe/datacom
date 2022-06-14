using System;

namespace Timesheet_Processor.Models
{
    public class PayRun
    {
        public int Id { get; set; }
        public string PayGroupId { get; set; }
        public string RunType { get; set; }
        public string Status { get; set; }
        public bool Locked { get; set; }
        public PayPeriod PayPeriod { get; set; }
        public string FinalisationDate { get; set; }
        public DateTime DCDate { get; set; }
        public bool ExcludeFromBanking { get; set; }
    }
}
