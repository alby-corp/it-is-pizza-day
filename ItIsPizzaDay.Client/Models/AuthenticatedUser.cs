using ItIsPizzaDay.Shared.Models;

namespace ItIsPizzaDay.Client.Models
{
    public class AuthenticatedUser
    {
        public AuthenticatedUser(string name, Token token, string role)
        {
            Name = name;
            Token = token;
            Role = role;
        }

        public string Name { get; }
        public Token Token { get; }
        public string Role { get; }
    }
}