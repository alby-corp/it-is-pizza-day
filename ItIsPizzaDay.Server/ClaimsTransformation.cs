namespace ItIsPizzaDay.Server
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Http;
    using Shared.Models;

    class ClaimsTransformation : IClaimsTransformation
    {
        readonly QueenMargheritaContext db;
        readonly IHttpContextAccessor _http;

        public ClaimsTransformation(QueenMargheritaContext db, IHttpContextAccessor http)
        {
            this.db = db;
            _http = http;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var id = principal.TryGetId();
            if (id == null)
            {
                return principal;
            }
            
            var user = await db.Muppet.FindAsync(id.Value);
            if (user == null)
            {
                user = new Muppet
                {
                    Id = id.Value,
                    RealName = principal.TryGetName() ?? "Unknown",
                    IsAdmin = false
                };
                
                db.Muppet.Add(user);
                await db.SaveChangesAsync();
            }

            var result = ((ClaimsIdentity)principal.Identity).Clone();
            
            result.AddClaim(new Claim(result.RoleClaimType, user.IsAdmin ? Role.Admin : Role.User));
            
            return new ClaimsPrincipal(result);
        }
    }
}