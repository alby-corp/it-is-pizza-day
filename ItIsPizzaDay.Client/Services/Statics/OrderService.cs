namespace ItIsPizzaDay.Client.Services.Statics
{
    using System.Collections.Generic;
    using ItIsPizzaDay.Shared.Models;

    public static class OrderService
    {
        public static Order GetOrder(Food food, ICollection<Ingredient> customIngredients = null) => new Order
        {
            FoodOrder = new List<FoodOrder>
            {
                FoodOrderService.GetFoodOrderGuidOnly(food, customIngredients)
            }
        };

        public static Order GetOrder(ICollection<FoodOrder> foodsOrder) => new Order
        {
            FoodOrder = FoodOrderService.ParseGuidOnly(foodsOrder)
        };
    }
}