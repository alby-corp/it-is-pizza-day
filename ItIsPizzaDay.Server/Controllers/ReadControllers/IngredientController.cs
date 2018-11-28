namespace ItIsPizzaDay.Server.Controllers.ReadControllers
{
    using Repositories;
    using Shared.Models;

    public class IngredientController : ReadEntityController<Ingredient>
    {
        public IngredientController(IngredientRepository repository)
            : base(repository)
        {
        }
    }
}