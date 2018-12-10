namespace ItIsPizzaDay.Client.Pages.ShopComponent
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ItIsPizzaDay.Shared.Models;
    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.AspNetCore.Blazor.Services;
    using Services.Abstract;
    using Services.Statics;
    using FoodType = ItIsPizzaDay.Shared.Models.Type;

    public class ShopComponent : BlazorComponent
    {
        [Inject]
        private IUriHelper UriHelper { get; set; }

        [Inject]
        private IReadService Reader { get; set; }

        [Inject]
        private IWriteService Writer { get; set; }

        [Inject]
        private ICartService CartService { get; set; }

        protected ICollection<FoodType> Types { get; private set; } = new List<FoodType>();

        protected override async Task OnParametersSetAsync()
        {
            Types = (await Reader.Type.GetAllAsync()).ToList();
        }

        protected async Task AddToCart(Food food)
        {
            var foodOrder = FoodOrderService.GetFoodOrder(food);

            await CartService.Add(foodOrder);

            UriHelper.NavigateTo("/cart");
        }

        protected async Task OrderNow(Food food)
        {
            var order = OrderService.GetOrder(food);

            await Writer.Order.Save(order);

            UriHelper.NavigateTo("/orders");
        }
    }
}