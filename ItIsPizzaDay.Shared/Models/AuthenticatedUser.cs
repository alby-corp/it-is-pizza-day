namespace ItIsPizzaDay.Shared.Models
{
    public class AuthenticatedUser
    {
        // For json deserialization
        public AuthenticatedUser()
        {
        }
        
        public AuthenticatedUser(string name, Token token, string role)
        {
            Name = name;
            Token = token;
            Role = role;
        }

        public string Name { get; set; }
        public Token Token { get; set; }
        public string Role { get; set; }
    }
}