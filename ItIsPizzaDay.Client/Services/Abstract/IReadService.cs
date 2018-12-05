namespace ItIsPizzaDay.Client.Services.Abstract
{
    using ItIsPizzaDay.Shared.Models;

    public interface IReadService
    {
        ReadEndPoint<Food> Food { get; }
        ReadEndPoint<Ingredient> Ingredient { get; }
        ReadEndPoint<Type> Type { get; }
    }
}