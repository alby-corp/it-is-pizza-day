namespace ItIsPizzaDay.Shared.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class UserSummary
    {
        public string UserName { get; set; }
        public IEnumerable<string> Foods { get; set; }
        public decimal Total { get; set; }

        public UserSummary(string username, ICollection<FoodOrder> foodOrders)
        {
            UserName = username;


            Foods = foodOrders.Select(fo =>
            {
                var supplements = fo.FoodOrderIngredient
                    .Where(foi => !foi.Isremoval)
                    .Select(foi => foi.IngredientNavigation.Name)
                    .ToList();

                var removals = fo.FoodOrderIngredient
                    .Where(foi => foi.Isremoval)
                    .Select(foi => foi.IngredientNavigation.Name)
                    .ToList();

                var supplementsText = supplements.Any()
                    ? $"Supp: {string.Join(";", supplements)}"
                    : string.Empty;

                var removalsText = removals.Any()
                    ? $"Rem: {string.Join(";", removals)}"
                    : string.Empty;

                var price = fo.Price() + 
                            fo.FoodOrderIngredient
                                .Where(foi => !foi.Isremoval)
                                .Sum(foi => foi.IngredientNavigation.Price);

                return $"{fo.FoodNavigation.Name}. {supplementsText} {removalsText} Price: {price}";
            });

            Total = foodOrders.Sum(fo => fo.FoodNavigation.Price) + foodOrders.SelectMany(fo => fo.FoodOrderIngredient)
                        .Sum(foi => foi.IngredientNavigation.Price ?? 0);
        }
    }
}