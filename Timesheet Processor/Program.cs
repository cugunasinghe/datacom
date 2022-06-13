using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Timesheet_Processor.Models;

namespace Timesheet_Processor
{
    class Program
    {
        private static ApiManager _apiManager;

        static void Main(string[] args)
        {
            // display application welcome message
            Console.WriteLine("--------------------------------------------\n");
            Console.WriteLine("            TIMESHEET PROCESSOR             \r");
            Console.WriteLine("--------------------------------------------\n");

            // requesting user for the company code, start date and the end date
            UserRequest.ReadUserInput();

            //create api manger instance
            _apiManager = new ApiManager(new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build());
        }
    }
}

