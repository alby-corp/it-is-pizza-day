namespace ItIsPizzaDay.Client.Models
{
    using System;

    public class Token
    {
        public Token(string value, DateTimeOffset expiration)
        {
            Value = value;
            Expiration = expiration;
        }

        public string Value { get; }
        
        public DateTimeOffset Expiration { get; }
    }
}