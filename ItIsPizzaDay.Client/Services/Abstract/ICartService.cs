namespace ItIsPizzaDay.Client.Services.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ItIsPizzaDay.Shared.Models;

    public interface ICartService
    {
        ICollection<FoodOrder> Value { get; }

        void Subscribe(IObserver<ICollection<FoodOrder>> observer);

        void Subscribe(Action<ICollection<FoodOrder>> onNext);

        Task Add(FoodOrder foodOrder);

        Task Delete(Guid id);

        Task Clear();
    }
}