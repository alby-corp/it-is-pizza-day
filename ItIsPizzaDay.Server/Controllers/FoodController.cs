namespace ItIsPizzaDay.Server.Controllers
{
    using Repositories;
    using Shared.Models;

    public class FoodController : EntityController<Food>
    {
        public FoodController(FoodRepository repository)
            : base(repository)
        {
        }
    }
}