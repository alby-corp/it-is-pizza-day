namespace ItIsPizzaDay.Client.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ItIsPizzaDay.Shared.Models;

    public interface IReadService
    {
        Task<IEnumerable<Type>> Types();
    }
}