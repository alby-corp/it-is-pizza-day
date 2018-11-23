namespace ItIsPizzaDay.Shared.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Ingredient
    {
        public Ingredient()
        {
            FoodOrderIngredient = new HashSet<FoodOrderIngredient>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }

        public ICollection<FoodOrderIngredient> FoodOrderIngredient { get; set; }
    }
}
