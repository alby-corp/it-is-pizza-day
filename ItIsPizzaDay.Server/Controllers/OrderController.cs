namespace ItIsPizzaDay.Server.Controllers
{
    using Repositories;
    using Shared.Models;

    public class OrderController : EntityController<Order>
    {
        public OrderController(OrderRepository repository)
            : base(repository)
        {
        }
    }
}