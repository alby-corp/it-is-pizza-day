namespace ItIsPizzaDay.Shared.Models
{
    using System;
    using Abstract;

    public class Entity : IEntity
    {
        public Guid Id { get; set; }
    }
}