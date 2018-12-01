namespace ItIsPizzaDay.Server.Repositories.Structure
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IReadRepository<TEntity>
    {
        Task<TEntity> Get(Guid id);

        Task<List<TEntity>> GetAll();
    }
}