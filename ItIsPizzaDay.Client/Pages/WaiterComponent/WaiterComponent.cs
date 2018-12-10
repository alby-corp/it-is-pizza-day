namespace ItIsPizzaDay.Client.Pages.WaiterComponent
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ItIsPizzaDay.Shared.Models;
    using Microsoft.AspNetCore.Blazor;
    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.AspNetCore.Blazor.Services;
    using Services.Abstract;
    using Services.Statics;

    public class WaiterComponent : BlazorComponent
    {
        protected ElementRef filterInput;

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

        protected ICollection<Ingredient> CustomIngredients { get; private set; } = new List<Ingredient>();

        protected decimal TotalPrice => CustomIngredients.Except(Food.FoodIngredient.Select(fi => fi.IngredientNavigation)).Sum(i => i.Price ?? 0) + Food.Price;

        protected string Filter { get; set; } = string.Empty;

        protected async Task FilterChanged()
        {
            Filter = await Interop.Dom.GetValue(filterInput);
        }

        protected override void OnParametersSet()
        {
            var originalIngredients = Food.FoodIngredient.Select(fi => fi.IngredientNavigation).ToList();

            CustomIngredients = originalIngredients.ToList();
            Ingredients = Ingredients.Except(originalIngredients.ToList()).ToList();
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
            await CartService.Add(FoodOrderService.GetFoodOrder(Food, CustomIngredients));

            UriHelper.NavigateTo("/cart");
        }

        protected async Task OrderNow()
        {
            await Writer.Order.Save(OrderService.GetOrder(Food, CustomIngredients));

            UriHelper.NavigateTo("/orders");
        }
    }
}