using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Timesheet_Processor.Models;

namespace Timesheet_Processor.Tests
{
    public class ApiManagerTests
    {
        private static ApiManager _apiManager;
        private static IConfiguration _configuration;

        [SetUp]
        public void Setup()
        {
            var inMemorySettings = new Dictionary<string, string> {
                                    {"baseUrl", "https://www.datapaylive.co.nz/demo"},
                                    {"clienId", "0826b206-6df7-4b34-bca0-60b8c42ccc44"},
                                    {"clientSecret", "fVnYxvsctf2wZBSEMnKKZefHEajO4UbaMNUJVUDJhvk=rY5Epmzetrnu90jMzp9sLxSZzoo="},
                                    {"tokenEndpoint", "https://auth.datapaylive.co.nz/connect/token"}
                                    };

            _configuration = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();
            _apiManager = new ApiManager(_configuration);
        }

        [Test]
        public void CompanyObjectShouldNotBeNull()
        {
            string companyCode = "TESTAPI";
            var responce = _apiManager.GetCompany(companyCode);
            Assert.NotNull(responce);
            Assert.IsInstanceOf(typeof(Company), responce);
        }

        [Test]
        public void CompanyObjectShouldBeNull()
        {
            string companyCode = "TESTAPI2";
            var responce = _apiManager.GetCompany(companyCode);
            Assert.IsNull(responce);
        }

        [Test]
        public void PayRunListShouldNotBeNullOrEmpty()
        {
            var startDate = new DateTime(2019, 9, 2);
            var endtDate = new DateTime(2019, 9, 15);

            var responce = _apiManager.GetPayRunList(startDate, endtDate);
            Assert.IsNotNull(responce);
            Assert.NotZero(responce.Count);
            Assert.IsInstanceOf(typeof(IList<PayRun>), responce);
        }

        [Test]
        public void PayRunListShouldBeEmpty()
        {
            var startDate = new DateTime(2019, 9, 2);
            var endtDate = new DateTime(2019, 9, 1);

            var responce = _apiManager.GetPayRunList(startDate, endtDate);
            Assert.Zero(responce.Count);
        }

        [Test]
        public void TimesheetShouldNotBeEmpty()
        {
            IList<PayRun> payrunList = new List<PayRun>
            {
                new PayRun() {Id =  26315},
                new PayRun() {Id =  26313}
            };

            var responce = _apiManager.GetTimesheetList(payrunList);
            Assert.IsNotNull(responce);
            Assert.NotZero(responce.Count);
            Assert.IsInstanceOf(typeof(IList<Timesheet>), responce);
        }

        [Test]
        public void TimesheetShouldBeEmpty()
        {
            IList<PayRun> payrunList = new List<PayRun>();
            var responce = _apiManager.GetTimesheetList(payrunList);
            Assert.Zero(responce.Count);
        }
    }
}