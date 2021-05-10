using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeGameCRAPI.Models;
using TradeGameCRAPI.Repositories;
using TradeGameCRAPI.Services;

namespace TradeGameCRAPI.Controllers
{
    public abstract class AppBaseController<TEntity, TEntityDTO, TEntityCreateDTO, TEntityUpdateDTO, TRepository> : ControllerBase
        where TEntity : class
        where TEntityDTO : BaseDTO
        where TEntityCreateDTO : class
        where TEntityUpdateDTO : class
        where TRepository : IBaseRepository<TEntity>
    {
        protected IBaseControllerService<
            TEntity,
            TEntityDTO,
            TEntityCreateDTO,
            TEntityUpdateDTO,
            TRepository
        > service;

        public AppBaseController(
            IBaseControllerService<
                TEntity,
                TEntityDTO,
                TEntityCreateDTO,
                TEntityUpdateDTO,
                TRepository
            > service
        )
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<TEntityDTO>>> Get()
        {
            var entitiesDto = await service.GetAll();

            return entitiesDto;
        }

        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<TEntityDTO>> Get(int id)
        {
            var entityDto = await service.Get(id);

            if (entityDto == null)
            {
                return NotFound();
            }

            return entityDto;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TEntityCreateDTO entityCreateDto)
        {
            var entityDto = await service.Add(entityCreateDto);

            return new CreatedAtRouteResult("Get", new { id = entityDto.Id }, entityDto);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] TEntityUpdateDTO entityUpdateDto)
        {
            await service.Update(entityUpdateDto);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<TEntityDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var entity = await service.FirstOrDefaultAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            var entityDto = service.ToEntityDTO(entity);

            patchDocument.ApplyTo(entityDto, ModelState);

            service.MapReverse(entityDto, entity);

            var isValid = TryValidateModel(entity);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            await service.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TEntityDTO>> Delete(int id)
        {
            await service.Delete(id);

            return NoContent();
        }
    }
}
