namespace ItIsPizzaDay.Client.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ItIsPizzaDay.Shared.Models;

    public interface IReadService
    {
        Task<ICollection<ItIsPizzaDay.Shared.Models.Type>> Types();

        Task<ICollection<Ingredient>> Ingredients();

        Task<Food> Food(Guid id);

        Task<ICollection<Food>> Foods();
    }
}