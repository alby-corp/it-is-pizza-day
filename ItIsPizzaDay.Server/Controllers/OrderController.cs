namespace ItIsPizzaDay.Server.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Repositories;
    using Shared.Models;

    public class OrderController : EntityController<Order>
    {
        public OrderController(OrderRepository repository)
            : base(repository)
        {
        }

        public override async Task<IActionResult> Create(Order entity)
        {
            var id = User.TryGetId();
            if (id == null)
            {
                return Unauthorized();
            }

            entity.Muppet = id.Value;
            
            return await base.Create(entity);
        }
    }
}