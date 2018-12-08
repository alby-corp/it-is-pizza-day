namespace ItIsPizzaDay.Client.Pages.ShopComponent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ItIsPizzaDay.Shared.Models;
    using Microsoft.AspNetCore.Blazor.Components;
    using Services.Abstract;
    using FoodType = ItIsPizzaDay.Shared.Models.Type;

    public class ShopComponent : BlazorComponent
    {
        [Inject]
        private IReadService Reader { get; set; }

        [Inject]
        private IWriteService Writer { get; set; }

        [Inject]
        private ICartService CartService { get; set; }

        protected ICollection<FoodType> Types { get; private set; } = new List<FoodType>();

        protected override async Task OnParametersSetAsync()
        {
            Types = (await Reader.Type.GetAllAsync()).ToList();
        }

        protected async Task OrderNow(Food food)
        {
            var order = new Order
            {
                FoodOrder = new List<FoodOrder>(new List<FoodOrder> { GetFoodOrder(food) })
            };

            await Writer.Order.Save(order);
        }

        protected async Task AddToCart(Food food)
        {
            var foodOrder = GetFoodOrder(food);

            Console.WriteLine(foodOrder.Food);

            await CartService.Add(foodOrder);
        }

        private static FoodOrder GetFoodOrder(Food food) => new FoodOrder
        {
            Food = food.Id,
            FoodNavigation = food
        };
    }
}