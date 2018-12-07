namespace ItIsPizzaDay.Client.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Subjects;
    using System.Threading.Tasks;
    using Abstract;
    using Blazor.Extensions.Storage;
    using ItIsPizzaDay.Shared.Models;

    public sealed class CartService : ICartService
    {
        private const string key = "f71cf4e3-a9b1-4852-a893-9f71a6399b4b";
        private readonly LocalStorage _localStorage;
        private readonly Task _initialized;

        private readonly BehaviorSubject<IEnumerable<FoodOrder>> _subject = new BehaviorSubject<IEnumerable<FoodOrder>>(new List<FoodOrder>());

        public IEnumerable<FoodOrder> Value => _subject.Value;

        public CartService(LocalStorage localStorage)
        {
            _localStorage = localStorage;
            _initialized = RepairFromStorage();
        }

        public void Subscribe(IObserver<IEnumerable<FoodOrder>> observer)
        {
            _subject.Subscribe(observer);
        }

        public void Subscribe(Action<IEnumerable<FoodOrder>> onNext)
        {
            _subject.Subscribe(onNext);
        }

        public async Task Add(FoodOrder foodOrder)
        {
            await _initialized;

            var foodsOrder = Value.ToList();
            foodsOrder.Add(foodOrder);

            _subject.OnNext(foodsOrder);
            await _localStorage.SetItem<IEnumerable<FoodOrder>>(key, foodsOrder);
        }

        public async Task Delete(Guid id)
        {
            await _initialized;

            var foodsOrder = Value.ToList();
            foodsOrder.Remove(new FoodOrder { Id = id });

            _subject.OnNext(foodsOrder);
            await _localStorage.SetItem(key, foodsOrder.Where(fo => fo.Id != id));
        }

        public async Task Clear()
        {
            _subject.OnNext(new List<FoodOrder>());
            await _localStorage.SetItem<IEnumerable<FoodOrder>>(key, new List<FoodOrder>());
        }

        private async Task RepairFromStorage()
        {
            var foodsOrder = await _localStorage.GetItem<IEnumerable<FoodOrder>>(key);

            _subject.OnNext(foodsOrder ?? new List<FoodOrder>());
        }
    }
}