namespace ItIsPizzaDay.Client.Services.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ItIsPizzaDay.Shared.Models;

    public interface ICartService
    {
        IEnumerable<FoodOrder> Value { get; }

        void Subscribe(IObserver<IEnumerable<FoodOrder>> observer);

        void Subscribe(Action<IEnumerable<FoodOrder>> onNext);

        Task Add(FoodOrder foodOrder);

        Task Delete(Guid id);

        Task Clear();
    }
}