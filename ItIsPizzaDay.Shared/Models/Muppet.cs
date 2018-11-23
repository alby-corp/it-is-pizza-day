namespace ItIsPizzaDay.Shared.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Muppet
    {
        public Muppet()
        {
            Order = new HashSet<Order>();
        }

        public Guid Id { get; set; }
        public string RealName { get; set; }

        public ICollection<Order> Order { get; set; }
    }
}
