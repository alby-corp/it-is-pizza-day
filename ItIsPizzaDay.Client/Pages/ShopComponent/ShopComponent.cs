namespace ItIsPizzaDay.Client.Pages.ShopComponent
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Blazor.Components;
    using Services;
    using FoodType = ItIsPizzaDay.Shared.Models.Type;

    public class ShopComponent : BlazorComponent
    {
        [Inject]
        private IReadService Reader { get; set; }

        protected IEnumerable<FoodType> Types { get; private set; } = new List<FoodType>();

        protected override async Task OnParametersSetAsync()
        {
            Types = (await Reader.Types()).ToList();
        }
    }
}