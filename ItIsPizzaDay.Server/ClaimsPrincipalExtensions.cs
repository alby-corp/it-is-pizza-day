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
    }
}