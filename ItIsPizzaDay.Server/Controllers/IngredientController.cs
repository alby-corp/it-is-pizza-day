namespace ItIsPizzaDay.Server.Controllers
{
    using Repositories;
    using Shared.Models;

    public class IngredientController : EntityController<Ingredient>
    {
        public IngredientController(IngredientRepository repository)
            : base(repository)
        {
        }
    }
}