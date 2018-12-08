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
        }
    }
}