namespace ItIsPizzaDay.Server.Controllers.ReadControllers
{
    using Repositories;
    using Shared.Models;

    public class FoodController : ReadEntityController<Food>
    {
        public FoodController(FoodRepository repository)
            : base(repository)
        {
        }
    }
}