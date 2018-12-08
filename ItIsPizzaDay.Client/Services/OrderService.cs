namespace ItIsPizzaDay.Client.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using ItIsPizzaDay.Shared.Models;

    public class OrderService
    {
        private readonly Food _food;
        private readonly ICollection<Ingredient> _originalIngredients;

        public OrderService(Food food)
        {
            _food = food;
            _originalIngredients = food.FoodIngredient.Select(fi => fi.IngredientNavigation).ToList();
        }

        public Order GetOrder(ICollection<Ingredient> customIngredients = null) => new Order
        {
            FoodOrder = new List<FoodOrder>
            {
                GetFoodOrderGuidOnly(customIngredients)
            }
        };

        public FoodOrder GetFoodOrder(ICollection<Ingredient> customIngredients = null)
        {
            var foodOrderIngredient = new List<FoodOrderIngredient>();

            if (customIngredients != null)
            {
                foodOrderIngredient.AddRange(customIngredients.Except(_originalIngredients).Select(ingredient => new FoodOrderIngredient
                {
                    Isremoval = false,
                    Ingredient = ingredient.Id,
                    IngredientNavigation = ingredient
                }).ToList());

                foodOrderIngredient.AddRange(_originalIngredients.Except(customIngredients).Select(ingredient => new FoodOrderIngredient
                {
                    Isremoval = true,
                    Ingredient = ingredient.Id,
                    IngredientNavigation = ingredient
                }).ToList());
            }

            var foodOrder = new FoodOrder
            {
                Food = _food.Id,
                FoodNavigation = _food,
                FoodOrderIngredient = foodOrderIngredient
            };

            return foodOrder;
        }

        private FoodOrder GetFoodOrderGuidOnly(ICollection<Ingredient> customIngredients = null)
        {
            var foodOrderIngredient = new List<FoodOrderIngredient>();

            if (customIngredients != null)
            {
                foodOrderIngredient.AddRange(customIngredients.Except(_originalIngredients).Select(ingredient => new FoodOrderIngredient
                {
                    Isremoval = false,
                    Ingredient = ingredient.Id
                }).ToList());

                foodOrderIngredient.AddRange(_originalIngredients.Except(customIngredients).Select(ingredient => new FoodOrderIngredient
                {
                    Isremoval = true,
                    Ingredient = ingredient.Id
                }).ToList());
            }

            var foodOrder = new FoodOrder
            {
                Food = _food.Id,
                FoodOrderIngredient = foodOrderIngredient
            };

            return foodOrder;
        }
    }
}