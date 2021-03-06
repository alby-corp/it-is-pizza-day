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
        private readonly string _key;
        private readonly LocalStorage _localStorage;
        private readonly Task _initialized;

        private readonly BehaviorSubject<ICollection<FoodOrder>> _subject = new BehaviorSubject<ICollection<FoodOrder>>(new List<FoodOrder>());

        public ICollection<FoodOrder> Value => _subject.Value.ToList();

        public CartService(LocalStorage localStorage, string key)
        {
            _localStorage = localStorage;
            _key = key;

            _initialized = RepairFromStorage();
        }

        public void Subscribe(IObserver<ICollection<FoodOrder>> observer)
        {
            _subject.Subscribe(observer);
        }

        public void Subscribe(Action<ICollection<FoodOrder>> onNext)
        {
            _subject.Subscribe(onNext);
        }

        public async Task Add(FoodOrder foodOrder)
        {
            await _initialized;

            var foodsOrder = Value.ToList();
            
            foodsOrder.Add(foodOrder);

            _subject.OnNext(foodsOrder);
            await _localStorage.SetItem<ICollection<FoodOrder>>(_key, foodsOrder);
        }

        public async Task Delete(Guid id)
        {
            await _initialized;

            var foodsOrder = Value.ToList();
            foodsOrder.Remove(new FoodOrder { Id = id });

            _subject.OnNext(foodsOrder);
            await _localStorage.SetItem(_key, foodsOrder.Where(fo => fo.Id != id));
        }

        public async Task Clear()
        {
            await _initialized;
            
            _subject.OnNext(new List<FoodOrder>());
            await _localStorage.SetItem<ICollection<FoodOrder>>(_key, new List<FoodOrder>());
        }
        
        private async Task RepairFromStorage()
        {
            var foodsOrder = await _localStorage.GetItem<ICollection<FoodOrder>>(_key);
            
            _subject.OnNext(foodsOrder ?? new List<FoodOrder>());
        }
    }
}