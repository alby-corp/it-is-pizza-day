namespace ItIsPizzaDay.Client.Pages
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ItIsPizzaDay.Shared.Models;
    using Microsoft.AspNetCore.Blazor.Components;
    using Services;

    public class IndexComponent : BlazorComponent
    {
        [Inject]
        private IReadService Reader { get; set; }

        protected IEnumerable<Type> Types { get; private set; } = new List<Type>();

        protected override async Task OnInitAsync()
        {
            Types = await Reader.Types();
        }
    }
}