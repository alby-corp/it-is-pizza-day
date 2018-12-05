namespace ItIsPizzaDay.Server.Repositories.Structure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Shared.Abstract;
    using Shared.Models;

    public abstract class Repository<T> : IRepository<T>
        where T : Entity, IEntity, new()
    {
        private readonly QueenMargheritaContext _context;

        protected abstract IQueryable<T> _selector(IQueryable<T> selector);

        private IQueryable<T> GetEntities() => _selector(_context.Set<T>());

        protected Repository(QueenMargheritaContext context)
        {
            _context = context;
        }

        public Task<T> Get(Guid id) => GetEntities().FirstOrDefaultAsync(e => e.Id == id);

        public Task<List<T>> GetAll() => GetEntities().ToListAsync();

        public async Task Create(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            _context.Remove(id);
            await _context.SaveChangesAsync();
        }
    }
}