namespace ItIsPizzaDay.Client.Models
{
    public class AuthenticatedUser
    {
        public AuthenticatedUser(string name, Token token)
        {
            Name = name;
            Token = token;
        }

        public string Name { get; }
        public Token Token { get; }
    }
}