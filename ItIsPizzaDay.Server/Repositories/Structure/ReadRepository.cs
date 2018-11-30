namespace ItIsPizzaDay.Server.Repositories.Structure
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Shared.Abstract;

    public abstract class ReadRepository<TEntity> : IReadRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        // ReSharper disable once InconsistentNaming
        protected readonly QueenMargheritaContext _context;

        protected abstract IQueryable<TEntity> _selector(IQueryable<TEntity> selector);

        protected ReadRepository(QueenMargheritaContext context)
        {
            _context = context;
        }

        public async Task<TEntity> Get(Guid id)
        {
            var entity = await _context.FindAsync<TEntity>(id);

            return entity;
        } 

        public IQueryable<TEntity> GetAll() => _selector(_context.Set<TEntity>());
    }
}