namespace ItIsPizzaDay.Shared.Models
{
    using System.Collections.Generic;

    public class Ingredient : Entity
    {
        public Ingredient()
        {
            FoodOrderIngredient = new HashSet<FoodOrderIngredient>();
        }

        public string Name { get; set; }
        public decimal? Price { get; set; }

        public ICollection<FoodOrderIngredient> FoodOrderIngredient { get; set; }
    }
}