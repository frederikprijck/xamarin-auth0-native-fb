using RestSharp;
using System;
using System.Threading.Tasks;

namespace Auth0.NativeLogin.Auth0
{
    public class Auth0Client
    {
        private readonly string clientId;
        private readonly string baseUrl;

        public Auth0Client(string domain, string clientId)
        {
            this.clientId = clientId;
            baseUrl = $"https://{domain}";
        }
        public async Task<TokenResponse> ExchangeFbToken(string audience, string token)
        {
            var options = new RestClientOptions($"{baseUrl}/oauth/token");
            var client = new RestClient(options);

            var request = new RestRequest()
                .AddBody(new
                {
                    grant_type = "urn:ietf:params:oauth:grant-type:token-exchange",
                    subject_token_type = "http://auth0.com/oauth/token-type/facebook-info-session-access-token",
                    audience = audience,
                    scope = "openid profile email",
                    subject_token = token,
                    client_id = clientId,
                });

            var result = await client.PostAsync<TokenResponse>(request).ConfigureAwait(false);
            return result;

        }
    }
}
