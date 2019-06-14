using System;
using System.Collections.Generic;
using System.Linq;

namespace ItIsPizzaDay.Shared.Models
{
    public class FoodOrder : Entity
    {
        public Guid? Food { get; set; }
        public Guid? Order { get; set; }
        public Food FoodNavigation { get; set; }
        public Order OrderNavigation { get; set; }
        public ICollection<FoodOrderIngredient> FoodOrderIngredient { get; set; } = new List<FoodOrderIngredient>();
    }

    public static class FoodOrderExtensions
    {
        public static decimal Price(this FoodOrder foodOrder) =>
            foodOrder.FoodOrderIngredient
                .Where(i => !i.Isremoval)
                .Sum(i => i.IngredientNavigation.Price ?? 0) + foodOrder.FoodNavigation.Price;

        public static IEnumerable<Ingredient> Ingredients(this FoodOrder foodOrder) =>
            foodOrder.FoodNavigation.FoodIngredient
                .Select(fi => fi.IngredientNavigation)
                .Except(foodOrder.FoodOrderIngredient.Where(foi => foi.Isremoval).Select(foi => foi.IngredientNavigation))
                .Concat(foodOrder.FoodOrderIngredient.Where(foi => !foi.Isremoval).Select(foi => foi.IngredientNavigation));

        public static string Key(this FoodOrder foodOrder) => 
            $"{foodOrder.FoodNavigation.Name};{string.Join(";", foodOrder.FoodOrderIngredient.Select(foi => foi.IngredientNavigation.Name))}";
    }
}