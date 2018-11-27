namespace ItIsPizzaDay.Shared.Models
{
    using System;

    public partial class FoodIngredient
    {
        public Guid Food { get; set; }
        public Guid Ingredient { get; set; }

        public Food FoodNavigation { get; set; }
        public Ingredient IngredientNavigation { get; set; }
    }
}
