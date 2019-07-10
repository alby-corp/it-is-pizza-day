namespace ItIsPizzaDay.Client.Shared
{
    using System.Threading.Tasks;
    using ItIsPizzaDay.Shared.Models;
    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.AspNetCore.Blazor.Layouts;
    using Models;
    using Services;
    using Services.Abstract;

    public class MainLayoutComponent : BlazorLayoutComponent
    {
        [Inject]
        AuthService AuthService { get; set; }
        
        [Inject]
        private ICartService CartService { get; set; }
        
        protected AuthenticatedUser User { get; set; }
        
        protected int TotalCartItems { get; set; }

        protected override void OnInit()
        {
            CartService.Subscribe(items =>
            {
                TotalCartItems = items.Count;
                StateHasChanged();
            });
        }

        protected override async Task OnInitAsync()
        {
            User = await AuthService.TryGetUserAsync();
        }

        protected void SignIn()
            => AuthService.SignIn();
        
        protected void SignOut()
            => AuthService.SignOut();
    }
}