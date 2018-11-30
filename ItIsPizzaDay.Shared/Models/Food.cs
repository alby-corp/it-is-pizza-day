namespace ItIsPizzaDay.Shared.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Food : Entity
    {
        public Food()
        {
            FoodIngredient = new HashSet<FoodIngredient>();
            FoodOrder = new HashSet<FoodOrder>();
        }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Visible { get; set; }
        public Guid Type { get; set; }
        
        public Type TypeNavigation { get; set; }

        public ICollection<FoodIngredient> FoodIngredient { get; set; }
        public ICollection<FoodOrder> FoodOrder { get; set; }
    }
}
