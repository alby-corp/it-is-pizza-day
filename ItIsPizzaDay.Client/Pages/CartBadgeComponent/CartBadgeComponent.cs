namespace ItIsPizzaDay.Client.Pages.CartBadgeComponent
{
    using Microsoft.AspNetCore.Blazor.Components;
    using Services.Abstract;

    public class CartBadgeComponent : BlazorComponent
    {
        [Inject]
        private ICartService CartService { get; set; }

        protected int TotalItems { get; set; }

        protected override void OnInit()
        {
            CartService.Subscribe(items =>
            {
                TotalItems = items.Count;
                StateHasChanged();
            });
        }
    }
}