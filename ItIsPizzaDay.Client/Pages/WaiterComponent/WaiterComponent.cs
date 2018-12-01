namespace ItIsPizzaDay.Client.Pages.WaiterComponent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ItIsPizzaDay.Shared.Models;
    using Microsoft.AspNetCore.Blazor.Components;

    public class WaiterComponent : BlazorComponent
    {
        [Parameter]
        protected ICollection<Ingredient> Ingredients { get; private set; } = new List<Ingredient>();

        [Parameter]
        private Food Food { get; set; } = new Food();

        protected FoodOrder OrderFood { get; } = new FoodOrder();

        protected IEnumerable<Ingredient> OrderIngredients => OrderFood.FoodNavigation.FoodIngredient.Select(fi => fi.IngredientNavigation).OrderBy(i => i.Name).ToList();

        protected decimal TotalPrice
        {
            get
            {
                Console.WriteLine("I WAS CALL");

                foreach (var i in OrderIngredients.OrderBy(i => i.Name))
                {
                    Console.WriteLine($"OrderIngredient {i.Name}");
                }

                foreach (var i in Food.FoodIngredient.Select(fi => fi.IngredientNavigation).OrderBy(i => i.Name))
                {
                    Console.WriteLine($"FoodIngredient {i.Name}");
                }

                return OrderIngredients.Except(Food.FoodIngredient.Select(fi => fi.IngredientNavigation)).Sum(i => i.Price ?? 0) + Food.Price;
            }
        }

        protected string Filter { get; set; } = "";

        protected override void OnParametersSet()
        {
            SetOrderFood();

            Ingredients = Ingredients.Except(OrderFood.FoodNavigation.FoodIngredient.Select(fi => fi.IngredientNavigation)).ToList();
        }

        protected void Add(Ingredient ingredient)
        {
            OrderFood.FoodNavigation.FoodIngredient.Add(GetFoodIngredient(ingredient));
            Ingredients.Remove(ingredient);
        }

        protected void Remove(Ingredient ingredient)
        {
            OrderFood.FoodNavigation.FoodIngredient.Remove(GetFoodIngredient(ingredient));
            Ingredients.Add(ingredient);
        }

        private void SetOrderFood()
        {
            OrderFood.Food = Food.Id;
            OrderFood.FoodNavigation = new Food { Name = Food.Name, Price = Food.Price, FoodIngredient = Food.FoodIngredient.Select(fi => fi).ToList() };
        }

        private FoodIngredient GetFoodIngredient(Ingredient ingredient) => new FoodIngredient
        {
            Food = Food.Id,
            Ingredient = ingredient.Id,
            IngredientNavigation = ingredient
        };
    }
}