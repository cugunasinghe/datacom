using System.Collections.Generic;

namespace Timesheet_Processor.Models
{
    public class Timesheet
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int PayRunId { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public IList<TimesheetValue> Values { get; set; }
    }
}
