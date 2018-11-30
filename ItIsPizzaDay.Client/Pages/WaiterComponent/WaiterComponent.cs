namespace ItIsPizzaDay.Client.Pages.WaiterComponent
{
    using System.Collections.Generic;
    using System.Linq;
    using ItIsPizzaDay.Shared.Models;
    using Microsoft.AspNetCore.Blazor.Components;

    public class WaiterComponent : BlazorComponent
    {
        [Parameter]
        protected ICollection<Ingredient> Ingredients { get; private set; } = new List<Ingredient>();

        [Parameter]
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        protected Food Food { get; private set; } = null;

        protected FoodOrder FoodOrder { get; } = new FoodOrder();

        protected ICollection<Ingredient> OrderIngredients { get; private set; }

        protected decimal TotalPrice() => OrderIngredients.Except(Food.FoodIngredient.Select(fi => fi.IngredientNavigation)).Sum(i => i.Price ?? 0) + Food.Price;

        protected override void OnParametersSet()
        {
            FoodOrder.FoodNavigation = Food;
            FoodOrder.Food = Food.Id;

            OrderIngredients = Food.FoodIngredient.Select(fi => fi.IngredientNavigation).ToList();

            Ingredients = Ingredients.Except(OrderIngredients).ToList();
        }

        protected void Add(Ingredient ingredient)
        {
            OrderIngredients.Add(ingredient);
            Ingredients.Remove(ingredient);
        }

        protected void Remove(Ingredient ingredient)
        {
            OrderIngredients.Remove(ingredient);
            Ingredients.Add(ingredient);
        }
    }
}