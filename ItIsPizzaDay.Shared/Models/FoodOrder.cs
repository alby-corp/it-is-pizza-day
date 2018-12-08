namespace ItIsPizzaDay.Shared.Models
{
    using System;
    using System.Collections.Generic;

    public partial class FoodOrder : Entity
    {
        public Guid? Food { get; set; }
        public Guid? Order { get; set; }

        public Food FoodNavigation { get; set; }
        public Order OrderNavigation { get; set; }
        public ICollection<FoodOrderIngredient> FoodOrderIngredient { get; set; }
    }
}
