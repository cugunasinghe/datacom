using RestSharp;
using RestSharp.Authenticators;
using System.Threading.Tasks;
using Timesheet_Processor.Models;

namespace Timesheet_Processor
{
    /// <summary>
    /// this authenticator will be used to fetch(if not already fetched) access token and append it in each api call
    /// </summary>
    public class ApiAuthenticator : AuthenticatorBase
    {
        string _tokenEndpoint, _clientId, _clientSecret;

        public ApiAuthenticator(string tokenEndpoint, string clientId, string clientSecret) : base("")
        {
            _tokenEndpoint = tokenEndpoint;
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        protected override async ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
        {
            var token = string.IsNullOrEmpty(Token) ? await GetToken() : Token;
            return new HeaderParameter(KnownHeaders.Authorization, token);
        }

        private async Task<string> GetToken()
        {
            var options = new RestClientOptions(_tokenEndpoint);
            using var client = new RestClient(options)
            {
                Authenticator = new HttpBasicAuthenticator(_clientId, _clientSecret),
            };

            var request = new RestRequest().AddParameter("grant_type", "client_credentials");
            var response = await client.PostAsync<TokenResponse>(request);
            return $"{response?.TokenType} {response?.AccessToken}";
        }
    }
}
