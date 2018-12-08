namespace ItIsPizzaDay.Client.Pages.WaiterComponent
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ItIsPizzaDay.Shared.Models;
    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.AspNetCore.Blazor.Services;
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

        protected ICollection<Ingredient> CustomIngredients { get; private set; } = new List<Ingredient>();

        private ICollection<Ingredient> OriginalIngredients { get; set; } = new List<Ingredient>();

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
            await CartService.Add(GetFoodOrder());

            UriHelper.NavigateTo("/cart");
        }
        
        protected async Task OrderNow()
        {
            var order = new Order
            {
                FoodOrder = new List<FoodOrder>
                {
                    GetFoodOrder()
                }
            };

            await Writer.Order.Save(order);
            
            UriHelper.NavigateTo("/orders");
        }
        
        private FoodOrder GetFoodOrder()
        {
            var foodOrderIngredient = CustomIngredients.Except(OriginalIngredients).Select(ingredient => new FoodOrderIngredient
            {
                Isremoval = false,
                Ingredient = ingredient.Id,
                IngredientNavigation = ingredient

            }).ToList();

            foodOrderIngredient.AddRange(OriginalIngredients.Except(CustomIngredients).Select(ingredient => new FoodOrderIngredient
            {
                Isremoval = true,
                Ingredient = ingredient.Id,
                IngredientNavigation = ingredient
            }).ToList());

            var foodOrder = new FoodOrder
            {
                Food = Food.Id,
                FoodNavigation = Food,
                FoodOrderIngredient = foodOrderIngredient
            };

            return foodOrder;
        }

    }
}

