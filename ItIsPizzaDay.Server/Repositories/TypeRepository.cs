namespace ItIsPizzaDay.Server.Repositories
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Shared.Models;
    using Structure;

    public class TypeRepository : ReadRepository<Type>
    {
        public TypeRepository(QueenMargheritaContext context)
            : base(context)
        {
        }

        protected override IQueryable<Type> _selector(IQueryable<Type> selector) => selector
            .Include(type => type.Food)
            .ThenInclude(food => food.FoodIngredient)
            .ThenInclude(fi => fi.IngredientNavigation);
    }
}