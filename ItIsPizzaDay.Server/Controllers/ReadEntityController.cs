namespace ItIsPizzaDay.Server.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Shared.Abstract;

    public abstract class EntityController<TEntity> : DefaultController
        where TEntity : class, IEntity, new()
    {
        private readonly QueenMargheritaContext _context;

        protected EntityController(QueenMargheritaContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual async Task<ActionResult<TEntity>> Get(Guid id)
        {
            var entity = await _context.FindAsync<TEntity>(id);
            return entity;
        }
        
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TEntity>>> Get()
        {
            var queryOption = ODataQuery.Parse<TEntity>(HttpContext.Request.QueryString.Value, oDataSettings);

            var result = await _context.Set<TEntity>()
                .Apply(queryOption)
                .ToListAsync(HttpContext.RequestAborted);
            
            return result;
        }

        [HttpPost]
        public virtual async Task<ActionResult<TEntity>> Save([FromBody] Patch<TEntity> patch)
        {
            var entity = patch.Apply(new DbContextPatcher(_context));

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status409Conflict);
            }

            return entity;
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<TEntity>> Delete(Guid id)
        {
            var entity = new TEntity
            {
                Id = id
            };

            _context.Set<TEntity>().Remove(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status409Conflict);
            }

            return entity;
        }
    }
}
