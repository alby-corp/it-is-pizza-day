namespace ItIsPizzaDay.Client.Pages.CartComponent
{
    using System;
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

        protected void Delete(Guid id)
        {
            CartService.Delete(id);
        }

        protected void DoOrder()
        {
            
        }

        protected void Clear()
        {
            CartService.Clear();
        }
    }
}