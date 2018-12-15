namespace ItIsPizzaDay.Client.Services
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.JSInterop;
    using Models;

    public class AuthService
    {
        private readonly AuthConfig _config;

        public AuthService(AuthConfig config)
        {
            _config = config;
        }

        private IJSInProcessRuntime JS => (IJSInProcessRuntime)JSRuntime.Current;

        public bool IsLoggedIn() =>
            JS.Invoke<bool>("interop.authentication.isLoggedIn", _config);
        
        public void SignIn() =>
            JS.Invoke<string>("interop.authentication.loginRedirect", _config);

        public void SignOut() =>
            JS.Invoke<string>("interop.authentication.logout", _config);

        public async Task<AuthenticatedUser> TryGetUserAsync()
        {
            var result = await JS.InvokeAsync<GetTokenResult>("interop.authentication.getTokenAsync", _config);
            if (result == null)
            {
                return null;
            }

            return new AuthenticatedUser(result.UserName, new Token(result.IdToken, result.Expires));

        }
        
        class GetTokenResult
        {
            public string IdToken { get; set; }
            
            public DateTime Expires { get; set; }
            
            public string UserName { get; set; }
        }
    }
}