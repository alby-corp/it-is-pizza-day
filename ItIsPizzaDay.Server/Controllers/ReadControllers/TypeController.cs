namespace ItIsPizzaDay.Server.Controllers.ReadControllers
{
    using Shared.Models;

    public class TypeController : ReadEntityController<Type>
    {
        public TypeController(QueenMargheritaContext context)
            : base(context)
        {
        }
    }
}