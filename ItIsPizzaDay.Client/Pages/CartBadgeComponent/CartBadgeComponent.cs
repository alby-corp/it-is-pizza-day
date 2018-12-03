namespace ItIsPizzaDay.Client.Pages.CartBadgeComponent
{
    using System.Linq;
    using Microsoft.AspNetCore.Blazor.Components;
    using Services;

    public class CartBadgeComponent : BlazorComponent
    {
        [Inject]
        private CartService CartService { get; set; }

        protected int TotalItems { get; set; }

        protected override void OnInit()
        {
            CartService.Subscribe(items =>
            {
                TotalItems = items.Count();
                StateHasChanged();
            });
        }
    }
}