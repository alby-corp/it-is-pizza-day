namespace ItIsPizzaDay.Server.Repositories
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Shared.Models;
    using Structure;

    public class FoodRepository : Repository<Food>
    {
        public FoodRepository(QueenMargheritaContext context)
            : base(context)
        {
        }

        protected override IQueryable<Food> _selector(IQueryable<Food> selector) => selector
            .Include(food => food.FoodIngredient)
            .ThenInclude(fi => fi.IngredientNavigation)
        ;
    }
}