namespace ItIsPizzaDay.Server.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Shared.Abstract;

    public abstract class ReadWriteEntityController<TEntity> : ReadEntityController<TEntity>
        where TEntity : class, IEntity, new()
    {
        private readonly QueenMargheritaContext _context;
        protected readonly LinkGenerator _linkGenerator;

        protected ReadWriteEntityController(QueenMargheritaContext context) : base(context)
        {
            _context = context;
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody]TEntity entity)
        {
            var result = await _context.Set<TEntity>().AddAsync(entity);

            await _context.SaveChangesAsync();
            
            return Created(result.Entity.Id.ToString(), result.Entity);
        }

        [HttpPut("{key}")]
        public virtual async Task<IActionResult> Put(Guid key, [FromBody]TEntity entity)
        {
            entity.Id = key;

            var result = _context.Set<TEntity>().Update(entity);            
            await _context.SaveChangesAsync();
            
            return Ok(result.Entity);
        }
        
        [HttpDelete("{key}")]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = new TEntity
            {
                Id = key
            };
            
            var result = _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();

            return Ok(result.Entity);
        }
    }
}
