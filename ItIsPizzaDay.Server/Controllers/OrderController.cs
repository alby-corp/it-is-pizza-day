namespace ItIsPizzaDay.Server.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Repositories;
    using Shared.Models;

    public class OrderController : EntityController<Order>
    {
        readonly OrderRepository repository;

        public OrderController(OrderRepository repository)
            : base(repository)
        {
            this.repository = repository;
        }

        [Authorize(Roles = Role.Admin)]
        public override Task<IActionResult> GetAll() 
            => base.GetAll();

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
        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IList<Order>>> GetOwnOrders()
        {
            var id = User.TryGetId();
            if (id == null)
            {
                return Unauthorized();
            }
            
            return await repository.GetAllByUser(id.Value);
        }
    }
}