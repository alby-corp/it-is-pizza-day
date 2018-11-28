namespace ItIsPizzaDay.Server.Repositories
{
    using System.Linq;
    using Shared.Models;
    using Structure;

    public class IngredientRepository : ReadRepository<Ingredient>
    {
        public IngredientRepository(QueenMargheritaContext context)
            : base(context)
        {
        }

        protected override IQueryable<Ingredient> _selector(IQueryable<Ingredient> selector) => selector;
    }
}