﻿namespace ItIsPizzaDay.Shared.Models
{
    using System;

    public partial class FoodOrderIngredient : Entity
    {
        public Guid FoodOrder { get; set; }
        public Guid Ingredient { get; set; }
        public bool Isremoval { get; set; }

        public FoodOrder FoodOrderNavigation { get; set; }
        public Ingredient IngredientNavigation { get; set; }
    }
}
