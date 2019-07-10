namespace ItIsPizzaDay.Client.Services
{
    using System.Threading.Tasks;
    using ItIsPizzaDay.Shared.Models;
    using Models;

    public interface ITokenSource
    {
        Task<Token> TryGetTokenAsync();
    }

    public static class TokenSource
    {
        public static ITokenSource From(Token token)
            => new Implementation(token);

        class Implementation : ITokenSource
        {
            readonly Token _token;

            public Implementation(Token token)
            {
                _token = token;
            }

            public Task<Token> TryGetTokenAsync() => Task.FromResult(_token);
        }
    }
}