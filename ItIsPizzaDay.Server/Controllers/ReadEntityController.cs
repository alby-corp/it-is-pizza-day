namespace ItIsPizzaDay.Server.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Shared.Abstract;

    public abstract class ReadEntityController<TEntity> : DefaultController
        where TEntity : class, IEntity, new()
    {
        private readonly QueenMargheritaContext _context;

        protected ReadEntityController(QueenMargheritaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get(Guid id)
        {
            var entity = await _context.FindAsync<TEntity>(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll() => Ok(await _context.Set<TEntity>().ToListAsync());
    }
}
