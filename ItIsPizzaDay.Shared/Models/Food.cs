namespace ItIsPizzaDay.Shared.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Food
    {
        public Food()
        {
            FoodOrder = new HashSet<FoodOrder>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Guid Type { get; set; }
        public bool? Visible { get; set; }

        public Type TypeNavigation { get; set; }
        public ICollection<FoodOrder> FoodOrder { get; set; }
    }
}
