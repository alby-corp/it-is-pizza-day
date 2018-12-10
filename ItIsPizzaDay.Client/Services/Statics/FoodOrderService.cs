namespace ItIsPizzaDay.Client.Services.Statics
{
    using System.Collections.Generic;
    using System.Linq;
    using ItIsPizzaDay.Shared.Models;

    public static class FoodOrderService
    {
        public static FoodOrder GetFoodOrder(Food food, ICollection<Ingredient> customIngredients = null)
        {
            var originalIngredients = GetOriginalIngredients(food);
            var foodOrderIngredient = new List<FoodOrderIngredient>();

            if (customIngredients != null)
            {
                foodOrderIngredient.AddRange(customIngredients.Except(originalIngredients).Select(ingredient => new FoodOrderIngredient
                {
                    Isremoval = false,
                    Ingredient = ingredient.Id,
                    IngredientNavigation = ingredient
                }).ToList());

                foodOrderIngredient.AddRange(originalIngredients.Except(customIngredients).Select(ingredient => new FoodOrderIngredient
                {
                    Isremoval = true,
                    Ingredient = ingredient.Id,
                    IngredientNavigation = ingredient
                }).ToList());
            }

            var foodOrder = new FoodOrder
            {
                Food = food.Id,
                FoodNavigation = food,
                FoodOrderIngredient = foodOrderIngredient
            };

            return foodOrder;
        }

        public static FoodOrder GetFoodOrderGuidOnly(Food food, ICollection<Ingredient> customIngredients = null)
        {
            var foodOrderIngredient = new List<FoodOrderIngredient>();

            var originalIngredients = GetOriginalIngredients(food);

            if (customIngredients != null)
            {
                foodOrderIngredient.AddRange(customIngredients.Except(originalIngredients).Select(ingredient => new FoodOrderIngredient
                {
                    Isremoval = false,
                    Ingredient = ingredient.Id
                }).ToList());

                foodOrderIngredient.AddRange(originalIngredients.Except(customIngredients).Select(ingredient => new FoodOrderIngredient
                {
                    Isremoval = true,
                    Ingredient = ingredient.Id
                }).ToList());
            }

            var foodOrder = new FoodOrder
            {
                Food = food.Id,
                FoodOrderIngredient = foodOrderIngredient
            };

            return foodOrder;
        }

        public static ICollection<FoodOrder> ParseGuidOnly(ICollection<FoodOrder> foodsOrder)
        {
            return foodsOrder.Select(fi =>
                new FoodOrder { Food = fi.Food, FoodOrderIngredient = fi.FoodOrderIngredient.Select(foi => new FoodOrderIngredient { Ingredient = foi.Ingredient, Isremoval = foi.Isremoval }).ToList() }).ToList();
        }

        private static ICollection<Ingredient> GetOriginalIngredients(Food food)
        {
            return food.FoodIngredient.Select(fi => fi.IngredientNavigation).ToList();
        }
    }
}