namespace ItIsPizzaDay.Client.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
//    using ItIsPizzaDay.Shared.Models;
    using Microsoft.AspNetCore.Blazor.Components;

    public class IndexComponent : BlazorComponent
    {
        [Inject]
        protected HttpClient Http { get; set; }
        
//        protected IEnumerable<Food> Foods { get; private set; } = new List<Food>();

        protected override async Task OnInitAsync()
        {
            var a  = await Http.GetAsync("http://localhost:5000/api/SampleData/Foods");
        }
    }
}