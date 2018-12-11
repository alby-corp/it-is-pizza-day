namespace ItIsPizzaDay.Shared.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class FoodOrder : Entity
    {
        public Guid? Food { get; set; }
        public Guid? Order { get; set; }

        public Food FoodNavigation { get; set; }
        public Order OrderNavigation { get; set; }
        public ICollection<FoodOrderIngredient> FoodOrderIngredient { get; set; } = new List<FoodOrderIngredient>();

        public decimal Price => FoodOrderIngredient.Where(i => !i.Isremoval).Sum(i => i.IngredientNavigation.Price ?? 0) + FoodNavigation.Price;

        public ICollection<string> Ingredients =>
            FoodNavigation.FoodIngredient.Select(fi => fi.IngredientNavigation.Name)
                .Except(FoodOrderIngredient.Where(foi => foi.Isremoval).Select(foi => foi.IngredientNavigation.Name))
                .Concat(FoodOrderIngredient.Where(foi => !foi.Isremoval).Select(foi => foi.IngredientNavigation.Name))
                .ToList();
    }
}