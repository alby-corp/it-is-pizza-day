namespace ItIsPizzaDay.Client.Pages.CartComponent
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ItIsPizzaDay.Shared.Models;
    using Microsoft.AspNetCore.Blazor.Components;
    using Services.Abstract;

    public class CartComponent : BlazorComponent
    {
        [Inject]
        private ICartService CartService { get; set; }

        protected ICollection<FoodOrder> FoodOrder { get; private set; }

        protected override void OnInit()
        {
            CartService.Subscribe(foodOrder =>
            {
                FoodOrder = foodOrder;
                StateHasChanged();
            });
        }

        protected async Task Delete(Guid id)
        {
            await CartService.Delete(id);
        }

        protected void DoOrder()
        {
            
        }

        protected async Task Clear()
        {
            await CartService.Clear();
        }
    }
}