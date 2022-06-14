using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using Timesheet_Processor.Models;

namespace Timesheet_Processor
{
    /// <summary>
    /// this manager class will be used to handle api calls
    /// </summary>
    public class ApiManager
    {
        readonly RestClient _client;
        string _baseUrl, _clientId, _clientSecret, _tokenEndpoint;

        public ApiManager(IConfiguration configuration)
        {
            _baseUrl = configuration.GetSection("baseUrl").Value;
            _clientId = configuration.GetSection("clienId").Value;
            _clientSecret = configuration.GetSection("clientSecret").Value;
            _tokenEndpoint = configuration.GetSection("tokenEndpoint").Value;

            var options = new RestClientOptions(_baseUrl);
            _client = new RestClient(options)
            {
                Authenticator = new ApiAuthenticator(_tokenEndpoint, _clientId, _clientSecret)
            };
        }

        public Company GetCompany(string companyCode)
        {
            try
            {
                IList<Company> response = _client.GetJson<IList<Company>>($"{_baseUrl}/api/v/1/companies/{companyCode}");
                return response.FirstOrDefault(x => x.Code == companyCode);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IList<PayRun> GetPayRunList(DateTime startDate, DateTime endDate)
        {
            try
            {
                IList<PayRun> response = _client.GetJson<IList<PayRun>>($"{_baseUrl}/api/v/1/payruns");
                return response.Where(x => x.PayPeriod?.StartDate >= startDate && x.PayPeriod?.EndDate <= endDate).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IList<Timesheet> GetTimesheetList(IList<PayRun> payrunList)
        {
            try
            {
                IList<Timesheet> response = _client.GetJson<IList<Timesheet>>($"{_baseUrl}/api/v/1/timesheets?type=API");
                return response.Where(x => payrunList.Any(y => y.Id == x.PayRunId)).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
