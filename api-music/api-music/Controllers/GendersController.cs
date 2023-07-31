using api_music.DTOs.GenderDTOs;
using api_music.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_music.Controllers
{
    [ApiController]
    [Route("api/genders")]

    public class GendersController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public GendersController(ApplicationDbContext context, IMapper mapper) 
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<GenderDTO>>> Get()
        {
            var entidades  = await context.Genders.ToListAsync();
            var dtos = mapper.Map<List<GenderDTO>>(entidades);
            return dtos;
        }
        [HttpGet("{id:int}", Name = "getGender")]
        public async Task<ActionResult<GenderDTO>> Get(int id)
        {
            var entidades = await context.Genders.FirstOrDefaultAsync(x => x.Id == id);
            if (entidades == null)
            {
                return NotFound();
            }
            var dto = mapper.Map<GenderDTO>(entidades);
            return dto;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GenderCreateDTO genderCreateDTO)
        {
            var entidad = mapper.Map<Gender>(genderCreateDTO);
            context.Add(entidad);
            await context.SaveChangesAsync();

            var genderDTO = mapper.Map<GenderDTO>(entidad);
            return new CreatedAtRouteResult("getGender", new {id=genderDTO.Id}, genderDTO);

        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] GenderCreateDTO genderCreateDTO)
        {
            var entidad = mapper.Map<Gender>(genderCreateDTO);
            entidad.Id = id;
            context.Entry(entidad).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await context.Genders.AnyAsync(x=>x.Id == id);
            if (!exist)
            {
                return NotFound();
            }
            context.Remove(new Gender() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();

        }

    }
}
