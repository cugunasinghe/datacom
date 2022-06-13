using Microsoft.Extensions.Configuration;

namespace Timesheet_Processor
{
    /// <summary>
    /// this manager class will be used to handle api calls
    /// </summary>
    public class ApiManager
    {
        string _baseUrl, _clientId, _clientSeecret;

        public ApiManager(IConfiguration configuration)
        {
            _baseUrl = configuration.GetSection("baseUrl").Value;
            _clientId = configuration.GetSection("clienId").Value;
            _clientSeecret = configuration.GetSection("clientSecret").Value;
        }
    }
}
