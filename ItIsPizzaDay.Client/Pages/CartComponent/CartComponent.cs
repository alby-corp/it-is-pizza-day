namespace ItIsPizzaDay.Client.Pages.CartComponent
{
    using System.Collections.Generic;
    using ItIsPizzaDay.Shared.Models;
    using Microsoft.AspNetCore.Blazor.Components;
    using Services;

    public class CartComponent : BlazorComponent
    {
        [Inject]
        private CartService CartService { get; set; }

        protected IEnumerable<FoodOrder> FoodOrder { get; private set; }

        protected override void OnInit()
        {
            CartService.Subscribe(foodOrder => FoodOrder = foodOrder);
        }
    }
}