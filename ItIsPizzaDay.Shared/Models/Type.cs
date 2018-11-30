namespace ItIsPizzaDay.Shared.Models
{
    using System.Collections.Generic;

    public class Type : Entity
    {
        public Type()
        {
            Food = new HashSet<Food>();
        }

        public string Description { get; set; }

        public ICollection<Food> Food { get; set; }
    }
}