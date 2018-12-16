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
            Foods = foods.Where(food => food.Type.ToString() == "2ee7bc5b-1ec1-4e4b-b457-1206fd1cbdd3").ToList();
        }
    }
}