namespace ItIsPizzaDay.Client.Services.Abstract
{
    using ItIsPizzaDay.Shared.Models;

    public interface IWriteService
    {
        WriteEndPoint<Order> Order { get; }
    }
}