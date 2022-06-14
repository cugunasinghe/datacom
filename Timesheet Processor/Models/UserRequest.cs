using System;

namespace Timesheet_Processor.Models
{
    public static class UserRequest
    {
        private static string _companyCode, _payPeriodStart, _payPeriodEnd;

        public static string GetCompanyCode()
        {
            return _companyCode;
        }

        public static string GetPayPeriodStartDateString()
        {
            return _payPeriodStart;
        }

        public static DateTime GetPayPeriodStartDate()
        {
            return DateTime.Parse(_payPeriodStart);
        }

        public static string GetPayPeriodEndDateString()
        {
            return _payPeriodEnd;
        }

        public static DateTime GetPayPeriodEndDate()
        {
            return DateTime.Parse(_payPeriodEnd);
        }

        public static void ReadUserInput()
        {
            SetCompanyCode();
            SetPayPeriodStartDate();
            SetPayPeriodEndDate();
        }

        private static void SetCompanyCode()
        {
            Console.WriteLine("Please enter the Company Code, and then press Enter");
            _companyCode = Console.ReadLine();
            ValidateCompanyCode(ref _companyCode);
        }

        private static void SetPayPeriodStartDate()
        {
            Console.WriteLine("Please enter pay period start date, and then press Enter");
            _payPeriodStart = Console.ReadLine();
            ValidateDate(ref _payPeriodStart);
        }

        private static void SetPayPeriodEndDate()
        {
            Console.WriteLine("Please enter pay period end date, and then press Enter");
            _payPeriodEnd = Console.ReadLine();
            ValidateDate(ref _payPeriodEnd);
        }

        // use pass by reference here so that the actual value will get updated by the changes wihin the method
        private static void ValidateCompanyCode(ref string companyCode)
        {
            bool isValid = false;
            while (!isValid)
            {
                if (string.IsNullOrWhiteSpace(companyCode))
                {
                    Console.WriteLine("Please enter a valid company code!");
                    companyCode = Console.ReadLine();
                }
                else
                {
                    isValid = true;
                }
            }
        }

        // use pass by reference here so that the actual value will get updated by the changes wihin the method
        private static void ValidateDate(ref string dateInput)
        {
            bool isValid = false;
            while (!isValid)
            {
                try
                {
                    // user has the flexibility to input any date format, and the system will convert it into the required format
                    DateTime tempDate = DateTime.Parse(dateInput);
                    dateInput = tempDate.ToString("yyyy-MM-dd");
                    isValid = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter a valid date!.. ex: 2022-01-01");
                    dateInput = Console.ReadLine();
                }
            }
        }
    }
}
