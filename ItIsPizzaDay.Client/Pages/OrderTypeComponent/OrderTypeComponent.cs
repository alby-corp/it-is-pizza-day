namespace ItIsPizzaDay.Client.Pages.OrderTypeComponent
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Blazor.Components;
    using Services;

    public class OrderTypeComponent : BlazorComponent
    {
        [Inject]
        private IReadService Reader { get; set; }

        protected IEnumerable<ItIsPizzaDay.Shared.Models.Type> Types { get; private set; } = new List<ItIsPizzaDay.Shared.Models.Type>();

        protected override async Task OnInitAsync()
        {
            Types = await Reader.Types();
        }

        protected void Next(Guid id)
        {
            
        }
    }
}