using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItIsPizzaDay.Client.Services.Abstract;
using ItIsPizzaDay.Shared.Models;
using Microsoft.AspNetCore.Blazor.Components;

namespace ItIsPizzaDay.Client.Pages.SummaryComponent
{
    public class SummaryComponent : BlazorComponent
    {
        [Inject] private IReadService Reader { get; set; }

        protected IEnumerable<Report> Summaries { get; private set; } =
            new List<Report>();

        protected override async Task OnInitAsync()
        {
            Summaries =
                from o in await Reader.Order.GetAllAsync()
                from fo in o.FoodOrder
                group fo by fo.Key()
                into g
                let header = g.First()
                select new Report(header.FoodNavigation,
                    header.FoodOrderIngredient.Where(i => i.Isremoval).Select(i => i.IngredientNavigation.Name),
                    header.FoodOrderIngredient.Where(i => !i.Isremoval).Select(i => i.IngredientNavigation.Name),
                    g.Count(),
                    g.Sum(o => o.Price()));
        }
    }
}