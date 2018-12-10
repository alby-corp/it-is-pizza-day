﻿namespace ItIsPizzaDay.Shared.Models
{
    using System;
    using System.Collections.Generic;

    public class Order : Entity
    {
        public DateTime? Date { get; set; }
        public Guid Muppet { get; set; }

        public Muppet MuppetNavigation { get; set; }
        public ICollection<FoodOrder> FoodOrder { get; set; } = new List<FoodOrder>();
    }
}