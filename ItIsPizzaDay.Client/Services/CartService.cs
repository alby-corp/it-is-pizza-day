namespace ItIsPizzaDay.Client.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Subjects;
    using ItIsPizzaDay.Shared.Models;

    public class CartService
    {
        private readonly BehaviorSubject<IEnumerable<FoodOrder>> _subject = new BehaviorSubject<IEnumerable<FoodOrder>>(new List<FoodOrder>());

        public IEnumerable<FoodOrder> Value => _subject.Value;

        public virtual void Subscribe(IObserver<IEnumerable<FoodOrder>> observer)
        {
            _subject.Subscribe(observer);
        }

        public virtual void Subscribe(Action<IEnumerable<FoodOrder>> onNext)
        {
            _subject.Subscribe(onNext);
        }

        public void Add(FoodOrder foodOrder)
        {
            var foodsOrder = Value.ToList();
            foodsOrder.Add(foodOrder);

            _subject.OnNext(foodsOrder);
        }

        public void Delete(Guid id)
        {
            var foodsOrder = Value.ToList();
            foodsOrder.Remove(new FoodOrder { Id = id });

            _subject.OnNext(foodsOrder);
        }

        public void Clear()
        {
            _subject.OnNext(new List<FoodOrder>());
        }
    }
}