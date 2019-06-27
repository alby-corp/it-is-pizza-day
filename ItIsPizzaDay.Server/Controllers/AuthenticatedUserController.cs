using ItIsPizzaDay.Client.Models;
using ItIsPizzaDay.Server.Repositories.Structure;
using ItIsPizzaDay.Shared.Models;

namespace ItIsPizzaDay.Server.Controllers
{
    public class AuthenticatedUserController : EntityController<AuthenticatedUser>
    {
        public AuthenticatedUserController(IRepository<AuthenticatedUser> repository) : base(repository)
        {
        }

        public AuthenticatedUser GetByToken()
        {
            return new AuthenticatedUser(User.TryGetName(), null, User.GetIsAdmin() ? Role.Admin : Role.User);
        }
    }
}