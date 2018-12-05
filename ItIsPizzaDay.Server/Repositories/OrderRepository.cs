namespace ItIsPizzaDay.Server.Repositories
{
    using System.Linq;
    using Shared.Models;
    using Structure;

    public class OrderRepository : Repository<Order>
    {
        public OrderRepository(QueenMargheritaContext context)
            : base(context)
        {
        }

        protected override IQueryable<Order> _selector(IQueryable<Order> selector) => selector;
    }
}