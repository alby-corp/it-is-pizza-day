namespace ItIsPizzaDay.Client.Pages.ShopComponent
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ItIsPizzaDay.Shared.Models;
    using Microsoft.AspNetCore.Blazor.Components;
    using Services.Abstract;
    using FoodType = ItIsPizzaDay.Shared.Models.Type;

    public class ShopComponent : BlazorComponent
    {
        [Inject]
        private IReadService Reader { get; set; }
        
        [Inject] IWriteService Writer { get; set; }

        protected ICollection<FoodType> Types { get; private set; } = new List<FoodType>();

        protected override async Task OnParametersSetAsync()
        {
            Types = (await Reader.Type.GetAllAsync()).ToList();
        }

        protected async Task OrderNow(Food food)
        {
            var order = new Order
            {
                FoodOrder = new List<FoodOrder>
                {
                    new FoodOrder
                    {
                        Food = food.Id,
                    }
                }
            };

            await Writer.Order.Save(order);
        }
    }
}