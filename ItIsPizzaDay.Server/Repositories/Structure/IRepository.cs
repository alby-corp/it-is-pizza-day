namespace ItIsPizzaDay.Server.Repositories.Structure
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRepository<T>
    {
        Task<T> Get(Guid id);

        Task<List<T>> GetAll();

        Task Create(T entity);

        Task Update(T entity);

        Task Delete(Guid id);
    }
}