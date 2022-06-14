namespace Timesheet_Processor.Models
{
    public class Company
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public string Display()
        {
            return $"Company Details -> \nName: {Name} \nCode: {Code}";
        }
    }
}
