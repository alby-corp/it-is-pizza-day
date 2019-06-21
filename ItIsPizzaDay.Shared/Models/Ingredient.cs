namespace ItIsPizzaDay.Shared.Models
{
    using System.Collections.Generic;

    public class Ingredient : Entity
    {
        public string Name { get; set; }
        public decimal? Price { get; set; }
    }
}