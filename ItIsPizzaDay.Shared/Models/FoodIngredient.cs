namespace ItIsPizzaDay.Shared.Models
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    public class FoodIngredient
    {
        public Guid Food { get; set; }
        public Guid Ingredient { get; set; }

        public Ingredient IngredientNavigation { get; set; }

        private bool Equals(FoodIngredient other) => Food.Equals(other.Food) && Ingredient.Equals(other.Ingredient);

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

            return obj.GetType() == GetType() && Equals((FoodIngredient) obj);
        }

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            unchecked
            {
                return (Food.GetHashCode() * 397) ^ Ingredient.GetHashCode();
            }
        }

        public static bool operator ==(FoodIngredient left, FoodIngredient right) => Equals(left, right);

        public static bool operator !=(FoodIngredient left, FoodIngredient right) => !Equals(left, right);
    }
}