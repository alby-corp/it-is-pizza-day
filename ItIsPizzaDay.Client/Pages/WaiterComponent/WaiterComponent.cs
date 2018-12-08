namespace ItIsPizzaDay.Client.Pages.WaiterComponent
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ItIsPizzaDay.Shared.Models;
    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.AspNetCore.Blazor.Services;
    using Services;
    using Services.Abstract;

    public class WaiterComponent : BlazorComponent
    {
        [Inject]
        private IUriHelper UriHelper { get; set; }

        [Inject]
        private ICartService CartService { get; set; }

        [Inject]
        private IWriteService Writer { get; set; }

        [Parameter]
        protected Food Food { get; set; } = new Food();

        [Parameter]
        protected ICollection<Ingredient> Ingredients { get; private set; } = new List<Ingredient>();

        private OrderService _builderServie;

        protected ICollection<Ingredient> CustomIngredients { get; private set; } = new List<Ingredient>();

        protected decimal TotalPrice => CustomIngredients.Except(Food.FoodIngredient.Select(fi => fi.IngredientNavigation)).Sum(i => i.Price ?? 0) + Food.Price;

        protected string Filter { get; set; } = string.Empty;

        protected override void OnParametersSet()
        {
            var originalIngredients = Food.FoodIngredient.Select(fi => fi.IngredientNavigation).ToList();

            CustomIngredients = originalIngredients.ToList();
            Ingredients = Ingredients.Except(originalIngredients.ToList()).ToList();

            _builderServie = new OrderService(Food);
        }

        protected void Add(Ingredient ingredient)
        {
            CustomIngredients.Add(ingredient);

            Ingredients.Remove(ingredient);
        }

        protected void Remove(Ingredient ingredient)
        {
            CustomIngredients.Remove(ingredient);

            Ingredients.Add(ingredient);
        }

        protected async Task AddToCart()
        {
            await CartService.Add(_builderServie.GetFoodOrder(CustomIngredients));

            UriHelper.NavigateTo("/cart");
        }

        protected async Task OrderNow()
        {
            await Writer.Order.Save(_builderServie.GetOrder(CustomIngredients));

            UriHelper.NavigateTo("/orders");
        }
    }
}