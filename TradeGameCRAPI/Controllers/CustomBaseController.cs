using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeGameCRAPI.Interfaces;
using TradeGameCRAPI.Models;

namespace TradeGameCRAPI.Controllers
{
    public abstract class CustomBaseController<TEntity, TEntityDTO, TEntityCreateDTO, TEntityUpdateDTO>
        : ControllerBase
        where TEntity : class
        where TEntityDTO : BaseDTO
        where TEntityCreateDTO : class
        where TEntityUpdateDTO : class
    {
        private readonly IRepository<TEntity> repository;
        private readonly IMapper mapper;
        private readonly MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg => {
            cfg.CreateMap<TEntity, TEntityDTO>().ReverseMap();
            cfg.CreateMap<TEntityCreateDTO, TEntity>();
            cfg.CreateMap<TEntityUpdateDTO, TEntity>().ReverseMap();
        });

        public CustomBaseController(IRepository<TEntity> repository)
        {
            this.repository = repository;
            mapper = mapperConfiguration.CreateMapper();
        }
        
        [HttpGet]
        public virtual async Task<ActionResult<List<TEntityDTO>>> Get()
        {
            var entities = await repository.GetAll();
            var entitiesDto = mapper.Map<List<TEntityDTO>>(entities);

            return entitiesDto;
        }

        [HttpGet("{id:int}")]
        public virtual async Task<ActionResult<TEntityDTO>> Get(int id)
        {
            var entity = await repository.Get(id);

            if (entity == null)
            {
                return NotFound();
            }

            var entityDto = mapper.Map<TEntityDTO>(entity);

            return entityDto;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TEntityCreateDTO entityCreateDto)
        {
            var entity = mapper.Map<TEntity>(entityCreateDto);

            await repository.Add(entity);

            var entityDto = mapper.Map<TEntityDTO>(entity);

            return new CreatedAtRouteResult(new { id = entityDto.Id }, entityDto);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] TEntityUpdateDTO entityUpdateDto)
        {
            var entity = mapper.Map<TEntity>(entityUpdateDto);

            await repository.Update(entity);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<TEntityUpdateDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var entity = await repository.Get(id);

            if (entity == null)
            {
                return NotFound();
            }

            var entityUpdateDto = mapper.Map<TEntityUpdateDTO>(entity);

            patchDocument.ApplyTo(entityUpdateDto, ModelState);
            mapper.Map(entityUpdateDto, entity);

            var isValid = TryValidateModel(entity);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            repository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TEntityDTO>> Delete(int id)
        {
            await repository.Delete(id);

            return NoContent();
        }
    }
}
