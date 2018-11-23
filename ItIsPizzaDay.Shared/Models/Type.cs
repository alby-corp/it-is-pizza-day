namespace ItIsPizzaDay.Shared.Models
{
    using System;
    using System.Collections.Generic;
    using Abstract;

    public partial class Type : IEntity
    {
        public Type()
        {
            Food = new HashSet<Food>();
        }

        public Guid Id { get; set; }
        public string Description { get; set; }

        public ICollection<Food> Food { get; set; }
    }
}
