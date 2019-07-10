namespace ItIsPizzaDay.Client.Services
{
    using System;
    using System.Threading.Tasks;
    using ItIsPizzaDay.Shared.Models;
    using Microsoft.JSInterop;
    using Models;

    public class AuthService : ITokenSource
    {
        private readonly AuthConfig _config;
        private readonly ReadServiceFactory _readerFactory;

        AuthenticatedUser _currentUser;
        
        public AuthService(AuthConfig config, ReadServiceFactory readerFactory)
        {
            _config = config;
            _readerFactory = readerFactory;
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
            if (_currentUser != null && _currentUser.Token.Expiration < DateTimeOffset.UtcNow)
            {
                return _currentUser;
            }
            
            var result = await JS.InvokeAsync<GetTokenResult>("interop.authentication.getTokenAsync", _config);
            if (result == null)
            {
                _currentUser = null;
                return null;
            }

            var token = new Token(result.IdToken, result.Expires);
            var reader = _readerFactory.Create(TokenSource.From(token));

            _currentUser = await reader.User.GetByTokenAsync();
            
            return _currentUser;
        }

        public async Task<Token> TryGetTokenAsync()
            => (await TryGetUserAsync())?.Token;
        
        class GetTokenResult
        {
            public string IdToken { get; set; }
            
            public DateTime Expires { get; set; }
            
            public string UserName { get; set; }
        }
    }
}