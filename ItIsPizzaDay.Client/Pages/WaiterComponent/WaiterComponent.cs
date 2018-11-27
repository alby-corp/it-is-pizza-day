namespace ItIsPizzaDay.Client.Pages.WaiterComponent
{
    using ItIsPizzaDay.Shared.Models;
    using Microsoft.AspNetCore.Blazor.Components;
    using Services;

    public class WaiterComponent : BlazorComponent
    {
        [Parameter]
        protected Food Food { get; private set; } = new Food();      
    }
}