namespace ItIsPizzaDay.Server.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Repositories.Structure;

    public abstract class ReadEntityController<TEntity> : DefaultController
    {
        private readonly IReadRepository<TEntity> _repository;

        protected ReadEntityController(IReadRepository<TEntity> repository)
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
    }
}