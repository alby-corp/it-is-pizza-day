namespace ItIsPizzaDay.Shared.Models
{
    using System;

    public class Token
    {
        // For json deserialization
        public Token()
        {
        }

        public Token(string value, DateTimeOffset expiration)
        {
            Value = value;
            Expiration = expiration;
        }

        public string Value { get; set; }
        
        public DateTimeOffset Expiration { get; set; }
    }
}