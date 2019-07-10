using ItIsPizzaDay.Shared.Models;

namespace ItIsPizzaDay.Server
{
    using System;
    using System.Security.Claims;

    public static class ClaimsPrincipalExtensions
    {
        public static Guid? TryGetId(this ClaimsPrincipal principal)
        {
            var id = principal.FindFirstValue("oid");
            if (Guid.TryParseExact(id, "d", out var result))
            {
                return result;
            }

            return null;
        }

        public static string TryGetName(this ClaimsPrincipal principal)
            => principal.FindFirstValue("name");       
        
        public static bool GetIsAdmin(this ClaimsPrincipal principal)
            => principal.IsInRole(Role.Admin); 
        
        public static DateTimeOffset GetExpiration(this ClaimsPrincipal principal)
        {
            var expiration = principal.FindFirstValue("exp");
            return DateTimeOffset.FromUnixTimeSeconds(long.Parse(expiration));
        }
    }
}