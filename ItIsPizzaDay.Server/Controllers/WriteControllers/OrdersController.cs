namespace ItIsPizzaDay.Server.Controllers
{
    using Shared.Models;

    public class OrdersController : ReadWriteEntityController<Order>
    {
        public OrdersController(QueenMargheritaContext context)
            : base(context)
        {
        }
    }
}