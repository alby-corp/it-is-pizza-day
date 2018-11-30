namespace ItIsPizzaDay.Client.Pages.ShopComponent
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ItIsPizzaDay.Shared.Models;
    using Microsoft.AspNetCore.Blazor.Components;
    using Services;
    using FoodType = ItIsPizzaDay.Shared.Models.Type;

    public class ShopComponent : BlazorComponent
    {
        [Inject]
        private IReadService Reader { get; set; }

        protected ICollection<FoodType> Types { get; private set; } = new List<FoodType>();
        protected ICollection<Ingredient> Ingredients { get; private set; } = new List<Ingredient>();
        protected Food Food { get; private set; } = null;

        protected override async Task OnParametersSetAsync()
        {
            Types = (await Reader.Types()).ToList();
            Ingredients = (await Reader.Ingredients()).OrderBy(i => i.Name).ToList();
        }

        protected void SetFood(Food food)
        {
            Food = food;
        }
    }
}