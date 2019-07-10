namespace ItIsPizzaDay.Client.Pages.MenuComponent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ItIsPizzaDay.Shared.Models;
    using Microsoft.AspNetCore.Blazor.Components;
    using Services.Abstract;

    public class MenuComponent : BlazorComponent
    {
        [Inject]
        private IReadService Reader { get; set; }

        [Parameter]
        private Guid Id { get; set; } = Guid.Empty;

        protected ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        
        protected ICollection<Food> Foods { get; set; } = new List<Food>();

        protected Food Food { get; set; } = new Food();

        protected override async Task OnInitAsync()
        {
            Ingredients = await Reader.Ingredient.GetAllAsync();
            
            var foods = await Reader.Food.GetAllAsync();
            Food = foods.First(food => food.Id == Id);
            
            // TODO :)
            var compatibleTypes = new[]
            {
                "32daf241-8c63-49a0-a22d-61057e46a730",
                "a9af080e-4a6a-4918-bbd2-834feeb36737",
                "0b9eec7f-cf8d-4771-87aa-51259e2fc816"
            };
            Foods = foods.Where(food => compatibleTypes.Contains(food.Type.ToString())).ToList();
            Console.WriteLine($"Fods: {Foods.Count}");
        }
    }
}