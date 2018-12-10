namespace ItIsPizzaDay.Client.Pages.CartComponent
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ItIsPizzaDay.Shared.Models;
    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.AspNetCore.Blazor.Services;
    using Services.Abstract;
    using Services.Statics;

    public class CartComponent : BlazorComponent
    {
        [Inject]
        private IUriHelper UriHelper { get; set; }
        
        [Inject]
        private ICartService CartService { get; set; }

        [Inject]
        private IWriteService Writer { get; set; }

        protected ICollection<FoodOrder> FoodsOrder { get; private set; } = new List<FoodOrder>();

        protected override void OnInit()
        {
            CartService.Subscribe(foodsOrder =>
            {
                FoodsOrder = foodsOrder;
                StateHasChanged();
            });
        }

        protected async Task Delete(Guid id)
        {
            await CartService.Delete(id);
        }

        protected async Task DoOrder()
        {
            await Writer.Order.Save(OrderService.GetOrder(FoodsOrder));

            await Clear();
            
            UriHelper.NavigateTo("/orders");
        }

        protected async Task Clear()
        {
            await CartService.Clear();
        }
    }
}