namespace ItIsPizzaDay.Server.Repositories.Structure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Shared.Abstract;

    public abstract class ReadRepository<TEntity> : IReadRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        // ReSharper disable once InconsistentNaming
        protected readonly QueenMargheritaContext _context;

        protected abstract IQueryable<TEntity> _selector(IQueryable<TEntity> selector);

        private IQueryable<TEntity> GetEntities() => _selector(_context.Set<TEntity>());

        protected ReadRepository(QueenMargheritaContext context)
        {
            _context = context;
        }

        public Task<TEntity> Get(Guid id) => GetEntities().FirstOrDefaultAsync(e => e.Id == id);

        public Task<List<TEntity>> GetAll() => GetEntities().ToListAsync();
    }
}