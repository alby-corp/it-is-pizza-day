namespace ItIsPizzaDay.Server.Repositories
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Shared.Models;
    using Structure;

    public class OrderRepository : Repository<Order>

    {
        public OrderRepository(QueenMargheritaContext context)
            : base(context)
        {
        }

        protected override IQueryable<Order> _selector(IQueryable<Order> selector) => selector
            .Include(o => o.FoodOrder).ThenInclude(fo => fo.FoodNavigation)
            .Include(o => o.FoodOrder).ThenInclude(fo => fo.FoodOrderIngredient).ThenInclude(foi => foi.IngredientNavigation);
    }
}