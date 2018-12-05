namespace ItIsPizzaDay.Server.Controllers
{
    using Repositories;
    using FoodType = Shared.Models.Type;

    public class TypeController : EntityController<FoodType>
    {
        public TypeController(TypeRepository repository)
            : base(repository)
        {
        }
    }
}