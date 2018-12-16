namespace ItIsPizzaDay.Server
{
    using System;

    public static class Heroku
    {
        public static string TryParseConnectionString(string value)
        {
            if (value == null)
            {
                return null;
            }
            
            if (!Uri.TryCreate(value, UriKind.Absolute, out var url))
            {
                return null;
            }
            
            var userInfo = url.UserInfo.Split(':');
            if (userInfo.Length != 2)
            {
                return null;
            }

            return $"Host={url.Host}; Port={url.Port}; Username={userInfo[0]}; Password={userInfo[1]}; Database={url.LocalPath.Substring(1)}; Pooling=true;";
        }
    }
}