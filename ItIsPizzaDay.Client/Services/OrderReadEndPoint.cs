namespace ItIsPizzaDay.Client.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using ItIsPizzaDay.Shared.Models;

    public class OrderReadEndPoint : ReadEndPoint<Order>
    {
        public OrderReadEndPoint(HttpClient http, Uri baseUrl, AuthService authService)
            : base(http, baseUrl, authService)
        {
        }
        
        public Task<ICollection<Order>> GetOwnOrdersAsync() 
            => GetJson<ICollection<Order>>("GetOwnOrders");
    }
}