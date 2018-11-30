namespace ItIsPizzaDay.Shared.Models
{
    using System;
    using Abstract;

    public class Entity : IEntity
    {
        protected bool Equals(Entity other) => Id.Equals(other.Id);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(objA: null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == this.GetType() && Equals((Entity) obj);
        }

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(Entity left, Entity right) => Equals(left, right);

        public static bool operator !=(Entity left, Entity right) => !Equals(left, right);

        public Guid Id { get; set; }
    }
}