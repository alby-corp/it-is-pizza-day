namespace ItIsPizzaDay.Client.Models
{
    using System.Collections.Generic;

    public class AuthConfig
    {
        public AuthConfig(string clientId, IReadOnlyList<string> scopes, string redirectUri, string postLogoutRedirectUri, string cacheLocation)
        {
            ClientId = clientId;
            Scopes = scopes;
            RedirectUri = redirectUri;
            PostLogoutRedirectUri = postLogoutRedirectUri;
            CacheLocation = cacheLocation;
        }

        public string ClientId { get; }
        
        public IReadOnlyList<string> Scopes { get; }
        
        public string RedirectUri { get; }
        
        public string PostLogoutRedirectUri { get; }
        
        public string CacheLocation { get; }
    }
}