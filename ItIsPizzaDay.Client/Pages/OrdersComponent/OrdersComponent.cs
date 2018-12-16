namespace ItIsPizzaDay.Client.Pages.OrdersComponent
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ItIsPizzaDay.Shared.Models;
    using Microsoft.AspNetCore.Blazor.Components;
    using Services.Abstract;

    public class OrdersComponent : BlazorComponent
    {
        [Inject]
        private IReadService Reader { get; set; }

        [Inject]
        private IWriteService Writer { get; set; }

        protected ICollection<Order> Orders { get; set; } = new List<Order>();

        protected override async Task OnInitAsync()
        {
            Orders = await Reader.Order.GetOwnOrdersAsync();
        }

        protected async Task Delete(Guid id)
        {
            await Writer.Order.Delete(id);

            Orders.Remove(new Order { Id = id });
        }
    }
}