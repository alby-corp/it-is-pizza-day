using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItIsPizzaDay.Client.Services.Abstract;
using ItIsPizzaDay.Client.Services.Statics;
using ItIsPizzaDay.Shared.Models;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Services;

namespace ItIsPizzaDay.Client.Pages.WaiterComponent
{
    using System;

    public class WaiterComponent : BlazorComponent
    {
        protected ElementRef filterInput;

        [Inject] private IUriHelper UriHelper { get; set; }

        [Inject] private ICartService CartService { get; set; }

        [Inject] private IWriteService Writer { get; set; }

        [Parameter] protected Food Food { get; set; } = new Food();

        [Parameter] protected ICollection<Food> Foods { get; set; } = new List<Food>();

        [Parameter] protected ICollection<Ingredient> Ingredients { get; private set; } = new List<Ingredient>();

        protected List<Ingredient> CustomIngredients { get; private set; } = new List<Ingredient>();

        protected decimal TotalPrice =>
            CustomIngredients.Except(Food.FoodIngredient.Select(fi => fi.IngredientNavigation)).Sum(i => i.Price ?? 0) +
            Food.Price;

        protected string Filter { get; set; } = string.Empty;

        protected ICollection<FoodOrder> BetterDeals { get; set; } = new List<FoodOrder>();

        protected ICollection<Ingredient> AllIngredients { get; private set; } = new List<Ingredient>();

        protected async Task FilterChanged()
        {
            Filter = await Interop.Dom.GetValue(filterInput);
        }

        protected override void OnParametersSet()
        {
            AllIngredients = Ingredients;

            var originalIngredients = Food.FoodIngredient.Select(fi => fi.IngredientNavigation).ToList();

            CustomIngredients = originalIngredients.ToList();
            Ingredients = Ingredients.Except(originalIngredients.ToList()).ToList();

            UpdateBetterDeals();
        }

        protected void Add(Ingredient ingredient)
        {
            CustomIngredients.Add(ingredient);

            Ingredients.Remove(ingredient);

            UpdateBetterDeals();
        }

        protected void Remove(Ingredient ingredient)
        {
            CustomIngredients.Remove(ingredient);

            Ingredients.Add(ingredient);

            UpdateBetterDeals();
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

        protected async Task AddToCart(FoodOrder food)
        {
            await CartService.Add(FoodOrderService.GetFoodOrder(food.FoodNavigation,
                food.FoodOrderIngredient.Select(ingredient => ingredient.IngredientNavigation).ToList()));

            UriHelper.NavigateTo("/cart");
        }

        protected async Task OrderNow(FoodOrder food)
        {
            await Writer.Order.Save(OrderService.GetOrder(food.FoodNavigation,
                food.FoodOrderIngredient.Select(ingredient => ingredient.IngredientNavigation).ToList()));

            UriHelper.NavigateTo("/orders");
        }

        private void UpdateBetterDeals()
        {
            // TODO :)
            var compatibleTypes = new[]
            {
                "32daf241-8c63-49a0-a22d-61057e46a730"
            };
            
            if (!compatibleTypes.Contains(Food.Type.ToString()))
            {
                return;
            }

            BetterDeals = FoodOrderService.LowestPrice(CustomIngredients, Foods)
                .Where(order => order.Price() < TotalPrice)
                .ToList();
        }
    }
}