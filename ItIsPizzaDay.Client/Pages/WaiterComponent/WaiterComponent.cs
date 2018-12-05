namespace ItIsPizzaDay.Client.Pages.WaiterComponent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ItIsPizzaDay.Shared.Models;
    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.AspNetCore.Blazor.Services;
    using Services;

    public class WaiterComponent : BlazorComponent
    {
        [Inject]
        private IUriHelper UriHelper { get; set; }

        [Inject]
        private CartService CartService { get; set; }

        [Parameter]
        protected Food Food { get; set; } = new Food();

        [Parameter]
        protected ICollection<Ingredient> Ingredients { get; private set; } = new List<Ingredient>();

        protected ICollection<Ingredient> CustomIngredients { get; private set; } = new List<Ingredient>();

        protected ICollection<Ingredient> OriginalIngredients { get; private set; } = new List<Ingredient>();

        protected decimal TotalPrice => CustomIngredients.Except(Food.FoodIngredient.Select(fi => fi.IngredientNavigation)).Sum(i => i.Price ?? 0) + Food.Price;

        protected string Filter { get; set; } = string.Empty;

        protected override void OnParametersSet()
        {
            OriginalIngredients = Food.FoodIngredient.Select(fi => fi.IngredientNavigation).ToList();
            CustomIngredients = OriginalIngredients.ToList();
            Ingredients = Ingredients.Except(OriginalIngredients.ToList()).ToList();
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
            var guid = Guid.NewGuid();

            var foodOrderIngredient = CustomIngredients.Except(OriginalIngredients).Select(ingredient => new FoodOrderIngredient
            {
                IngredientNavigation = ingredient,
                Isremoval = false,
                Id = Guid.NewGuid()
            }).ToList();

            foodOrderIngredient.AddRange(OriginalIngredients.Except(CustomIngredients).Select(ingredient => new FoodOrderIngredient
            {
                IngredientNavigation = ingredient,
                Isremoval = true,
                Id = Guid.NewGuid()
            }).ToList());

            var foodOrder = new FoodOrder
            {
                Id = guid,
                Food = Food.Id,
                FoodNavigation = Food,
                FoodOrderIngredient = foodOrderIngredient
            };

            await CartService.Add(foodOrder);

            UriHelper.NavigateTo("/cart");
        }

        protected void OrderNow()
        {
            // TODO: Call Writer Service;
        }
    }
}