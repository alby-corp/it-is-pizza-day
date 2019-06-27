namespace ItIsPizzaDay.Client.Services
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.JSInterop;
    using Models;

    public class AuthService
    {
        private readonly AuthConfig _config;
        private readonly ReadService _reader;

        public AuthService(AuthConfig config, ReadService reader)
        {
            _config = config;
            _reader = reader;
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

            var user = await _reader.User.GetByTokenAsync();

            return new AuthenticatedUser(result.UserName, new Token(result.IdToken, result.Expires), user.Role);
        }
        
        class GetTokenResult
        {
            public string IdToken { get; set; }
            
            public DateTime Expires { get; set; }
            
            public string UserName { get; set; }
        }
    }
}