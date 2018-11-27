namespace ItIsPizzaDay.Server.Controllers.ReadControllers
{
    using Repositories;
    using FoodType = Shared.Models.Type;

    public class TypeController : ReadEntityController<FoodType>
    {
        public TypeController(TypeRepository repository)
            : base(repository)
        {
        }
    }
}