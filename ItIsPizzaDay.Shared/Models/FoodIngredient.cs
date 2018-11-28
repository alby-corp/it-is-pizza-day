namespace ItIsPizzaDay.Shared.Models
{
    using System;

    public class FoodIngredient
    {
        public Guid Food { get; set; }
        public Guid Ingredient { get; set; }

        public Ingredient IngredientNavigation { get; set; }
    }
}
