namespace ItIsPizzaDay.Server.Repositories.Structure
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IReadRepository<TEntity>
    {
        Task<TEntity> Get(Guid id);

        IQueryable<TEntity> GetAll();
    }
}