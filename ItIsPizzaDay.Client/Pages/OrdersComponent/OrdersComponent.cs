namespace ItIsPizzaDay.Client.Pages.OrdersComponent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ItIsPizzaDay.Shared.Models;
    using Microsoft.AspNetCore.Blazor.Components;
    using Services.Abstract;

    public class OrdersComponent : BlazorComponent
    {
        [Inject]
        private IReadService Reader { get; set; }

        protected ICollection<Order> Orders { get; set; } = new List<Order>();

        protected override async Task OnInitAsync()
        {
            Orders = (await Reader.Order.GetAllAsync()).ToList();

            foreach (var order in Orders)
            {
                Console.WriteLine($"ORDER: {order.Id}");

                foreach (var fo in order.FoodOrder)
                {
                    Console.WriteLine($"FOOD: {fo.FoodNavigation.Name}");

                    foreach (var i in fo.Ingredients)
                    {
                        Console.WriteLine($"ING: {i}");
                    }

                    foreach (var foi in fo.FoodOrderIngredient)
                    {
                        Console.WriteLine($"INGREDIENT: {foi.IngredientNavigation.Name} {foi.Isremoval}");
                    }
                }
            }
        }

        protected void Delete(Guid id)
        {
        }
    }
}