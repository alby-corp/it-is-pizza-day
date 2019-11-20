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


            Foods = foodOrders.Select(fo => $"{fo.FoodNavigation.Name}. " +
                                               $"Supp: {string.Join(";", fo.FoodOrderIngredient.Where(foi => !foi.Isremoval).Select(foi => foi.IngredientNavigation.Name))}" +
                                               $"Rem: {string.Join(";", fo.FoodOrderIngredient.Where(foi => foi.Isremoval).Select(foi => foi.IngredientNavigation.Name))}");
            
            Total = foodOrders.Sum(fo => fo.FoodNavigation.Price) + foodOrders.SelectMany(fo => fo.FoodOrderIngredient)
                        .Sum(foi => foi.IngredientNavigation.Price ?? 0);
        }
    }
}