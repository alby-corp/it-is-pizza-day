namespace ItIsPizzaDay.Shared.Models
{
    using System.Collections.Generic;

    public partial class Muppet : Entity
    {
        public Muppet()
        {
            Order = new HashSet<Order>();
        }

        public string RealName { get; set; }

        public ICollection<Order> Order { get; set; }
        
        public bool IsAdmin { get; set; }
    }
}
