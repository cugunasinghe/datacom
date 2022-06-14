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

            try
            {
                // get company details
                Company company = _apiManager.GetCompany(UserRequest.GetCompanyCode());
                string message = company != null ? company.Display() : "Company Details Could not be found for the provided Company Code!";
                PrintMessage(message);

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
                        PrintMessage("Timesheet details could not be found for the selected PayRuns");
                    }
                }
                else
                {
                    PrintMessage("PayRun details could not be found for the given dates:");
                    PrintMessage($"{UserRequest.GetPayPeriodStartDateString()} to {UserRequest.GetPayPeriodEndDateString()}!");
                }

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine($"\n{ex.Message}");
                Console.ForegroundColor = ConsoleColor.White;
            }
            finally
            {
                //end of application
                Console.WriteLine("\n---------------------END-----------------------\r");
            }
        }

        private static void PrintMessage(string message)
        {
            Console.WriteLine($"\n{message}");
        }
    }
}

