namespace ItIsPizzaDay.Server.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Repositories.Structure;

    public abstract class EntityController<TEntity> : DefaultController
    {
        private readonly IRepository<TEntity> _repository;

        protected EntityController(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get(Guid id)
        {
            var entity = await _repository.Get(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll() => Ok(await _repository.GetAll());

        [HttpPost]
        public virtual async Task<IActionResult> Create([FromBody] TEntity entity)
        {
            await _repository.Create(entity);
            return Ok(entity);
        }

        [HttpPut]
        public virtual async Task<IActionResult> Update([FromBody] TEntity entity)
        {
            await _repository.Update(entity);
            return Ok(entity);
        }

        [HttpDelete]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            await _repository.Delete(id);
            return Ok(id);
        }
    }
}