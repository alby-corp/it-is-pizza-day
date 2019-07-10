namespace ItIsPizzaDay.Client.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using ItIsPizzaDay.Shared.Models;
    using Models;

    public class OrderReadEndPoint : ReadEndPoint<Order>
    {
        public OrderReadEndPoint(HttpClient http, ApiConfig config, ITokenSource tokenSource)
            : base(http, config, tokenSource)
        {
        }
        
        public Task<ICollection<Order>> GetOwnOrdersAsync() 
            => GetJson<ICollection<Order>>("GetOwnOrders");
    }
}