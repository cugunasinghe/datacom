using System;
using Timesheet_Processor.Models;

namespace Timesheet_Processor
{
    class Program
    {
        static void Main(string[] args)
        {
            // display application welcome message
            Console.WriteLine("--------------------------------------------\n");
            Console.WriteLine("            TIMESHEEET PROCESSOR            \r");
            Console.WriteLine("--------------------------------------------\n");

            // requesting user for the company code, start date and the end date
            UserRequest.ReadUserInput();

            Console.WriteLine(UserRequest.GetCompanyCode());
            Console.WriteLine(UserRequest.GetPayPeriodStartDate());
            Console.WriteLine(UserRequest.GetPayPeriodEndDate());
        }
    }
}

