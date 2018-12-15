namespace ItIsPizzaDay.Client.Shared
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.AspNetCore.Blazor.Layouts;
    using Models;
    using Services;

    public class MainLayoutComponent : BlazorLayoutComponent
    {
        [Inject]
        AuthService AuthService { get; set; }
        
        protected AuthenticatedUser User { get; set; }

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