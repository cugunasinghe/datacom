using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using Timesheet_Processor.Models;

namespace Timesheet_Processor
{
    class Program
    {
        private static IConfiguration _configuration;
        private static ApiManager _apiManager;
        private static ReportManager _reportManager;

        static void Main(string[] args)
        {
            //create api manger instance
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
            _apiManager = new ApiManager(_configuration);
            _reportManager = new ReportManager();

            // display application welcome message
            Console.WriteLine("--------------------------------------------\n");
            Console.WriteLine("            TIMESHEET PROCESSOR             \r");
            Console.WriteLine("--------------------------------------------\n");

            // requesting user for the company code, start date and the end date
            UserRequest.ReadUserInput();

            // get company details
            Company company = _apiManager.GetCompany(UserRequest.GetCompanyCode());
            Console.WriteLine(company != null ? company.Display() : "\nCompany Details Could not be found!");

            // get payrun details list
            IList<PayRun> payrunList = _apiManager.GetPayRunList(UserRequest.GetPayPeriodStartDate(), UserRequest.GetPayPeriodEndDate());

            // check if any payrun details are available for the givedn date range, else terminate application
            if (payrunList != null && payrunList.Any())
            {
                IList<Timesheet> timesheetList = _apiManager.GetTimesheetList(payrunList);
                if (timesheetList != null && timesheetList.Any())
                {
                    _reportManager.GenerateReport(timesheetList);
                }
                else
                {
                    Console.WriteLine($"\nTimesheet Details Could not be found for the selected PayRuns!");
                }
            }
            else
            {
                Console.WriteLine($"\nPayRun Details Could not be found for the dates:");
                Console.WriteLine($"{UserRequest.GetPayPeriodStartDateString()} to {UserRequest.GetPayPeriodEndDateString()}!");
            }

            //end of application
            Console.WriteLine("\n---------------------END-----------------------\r");
        }
    }
}

