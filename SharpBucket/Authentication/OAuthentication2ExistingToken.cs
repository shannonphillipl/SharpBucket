using RestSharp;
using RestSharp.Authenticators;

namespace SharpBucket.Authentication
{
    public class OAuthentication2ExistingToken : Authenticate
    {
        private const string TokenType = "Bearer";
        public OAuthentication2ExistingToken(string token, string baseUrl)
        {
            client = new RestClient(baseUrl)
            {
                Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, TokenType)
            };
        }
    }
}