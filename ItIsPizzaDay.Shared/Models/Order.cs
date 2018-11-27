namespace ItIsPizzaDay.Shared.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Order
    {
        public Order()
        {
            FoodOrder = new HashSet<FoodOrder>();
        }

        public Guid Id { get; set; }
        public Guid? MuppetOld { get; set; }
        public DateTime? Date { get; set; }
        public Guid Muppet { get; set; }

        public Muppet MuppetNavigation { get; set; }
        public ICollection<FoodOrder> FoodOrder { get; set; }
    }
}
