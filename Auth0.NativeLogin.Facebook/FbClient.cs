using RestSharp;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Auth0.NativeLogin.Facebook
{
    public class FbClient
    {
        private string baseUrl;
        public FbClient()
        {
            baseUrl = $"https://graph.facebook.com";
        }

        public async Task<string> getSessionInfoAccessToken(string clientId, string fbToken)
        {
            var url = $"https://graph.facebook.com/v5.0/oauth/access_token?grant_type=fb_attenuate_token&client_id={clientId}&fb_exchange_token={fbToken}";
            var options = new RestClientOptions($"{baseUrl}/oauth/access_token")
            {
                ThrowOnAnyError = true,
                MaxTimeout = 5000
            };
            var client = new RestClient(options);

            var request = new RestRequest()
                .AddQueryParameter("grant_type", "fb_attenuate_token")
                .AddQueryParameter("client_id", clientId)
                .AddQueryParameter("fb_exchange_token", fbToken);


            var result = await client.GetAsync<TokenResponse>(request).ConfigureAwait(false);

            return result.AccessToken;
        }
        public async Task<User> ReadUserProfile(string fbToken)
        {
            var options = new RestClientOptions($"{baseUrl}/me")
            {
                ThrowOnAnyError = true,
                MaxTimeout = 5000
            };
            var client = new RestClient(options);

            var request = new RestRequest()
                .AddQueryParameter("access_token", fbToken);


            var result = await client.GetAsync<User>(request).ConfigureAwait(false);

            return result;

        }

        public class TokenResponse
        {
            [JsonPropertyName("access_token")]
            public string AccessToken { get; set; }
        }
    }
}
