namespace ItIsPizzaDay.Client.Pages.ShopComponent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ItIsPizzaDay.Shared.Models;
    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.AspNetCore.Blazor.Services;
    using Services;
    using Services.Abstract;
    using FoodType = ItIsPizzaDay.Shared.Models.Type;

    public class ShopComponent : BlazorComponent
    {
        [Inject]
        private IUriHelper UriHelper { get; set; }
        
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

        protected async Task AddToCart(Food food)
        {
            var foodOrder = new OrderService(food).GetFoodOrder();

            await CartService.Add(foodOrder);
            
            UriHelper.NavigateTo("/cart");
        }

        protected async Task OrderNow(Food food)
        {
            var order = new OrderService(food).GetOrder();
            
            await Writer.Order.Save(order);
            
            UriHelper.NavigateTo("/orders");
        }
    }
}