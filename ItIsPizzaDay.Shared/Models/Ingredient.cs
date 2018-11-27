namespace ItIsPizzaDay.Shared.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Ingredient
    {
        public Ingredient()
        {
            FoodIngredient = new HashSet<FoodIngredient>();
            FoodOrderIngredient = new HashSet<FoodOrderIngredient>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }

        public ICollection<FoodIngredient> FoodIngredient { get; set; }
        public ICollection<FoodOrderIngredient> FoodOrderIngredient { get; set; }
    }
}
