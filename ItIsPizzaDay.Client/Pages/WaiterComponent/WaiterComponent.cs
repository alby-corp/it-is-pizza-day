namespace ItIsPizzaDay.Client.Pages.WaiterComponent
{
    using System.Collections.Generic;
    using ItIsPizzaDay.Shared.Models;
    using Microsoft.AspNetCore.Blazor.Components;
    using Services;

    public class WaiterComponent : BlazorComponent
    {
        [Inject]
        private IReadService Reader { get; set; }
        
        [Parameter]
        protected Food Food { get; private set; } = new Food();

        [Parameter]
        protected IEnumerable<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}