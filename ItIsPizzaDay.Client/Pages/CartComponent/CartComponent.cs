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

        protected ICollection<FoodOrder> FoodOrder { get; private set; } = new List<FoodOrder>();

        protected override void OnInit()
        {
            Console.WriteLine("Roald 0");
            
            CartService.Subscribe(foodOrder =>
            {
                Console.WriteLine("Alby 1");

                FoodOrder = foodOrder;
                StateHasChanged();
                
                Console.WriteLine("Alby 2");
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