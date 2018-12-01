namespace ItIsPizzaDay.Client.Pages.MenuComponent
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ItIsPizzaDay.Shared.Models;
    using Microsoft.AspNetCore.Blazor.Components;
    using Services;

    public class MenuComponent : BlazorComponent
    {
        [Inject]
        private IReadService Reader { get; set; }

        [Parameter]
        private Guid Id { get; set; } = Guid.Empty;

        protected ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        protected Food Food { get; set; } = new Food();

        protected override async Task OnParametersSetAsync()
        {
            Ingredients = await Reader.Ingredients();
            Food = await Reader.Food(Id);
        }
    }
}