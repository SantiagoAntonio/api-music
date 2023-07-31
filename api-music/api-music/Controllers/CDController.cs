using api_music.DTOs.CDDTOS;
using api_music.DTOs.MemberDTOs;
using api_music.Entities;
using api_music.Services;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace api_music.Controllers
{
    [ApiController]
    [Route("api/cds")]
    public class CDController :ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IFileUploader fileUploader;
        private readonly string container = "cds";

        public CDController(ApplicationDbContext context, IMapper mapper,
            IFileUploader fileUploader)
        {
            this.context = context;
            this.mapper = mapper;
            this.fileUploader = fileUploader;
        }
        [HttpGet]
        public async Task<ActionResult<List<CDDTO>>> Get()
        {
            var cds = await context.CDs.ToListAsync();
            return mapper.Map<List<CDDTO>>(cds);
        }
        [HttpGet("{id}", Name = "getCD")]
        public async Task<ActionResult<CDDTO>> Get(int id)
        {
            var cd = await context.CDs.FirstOrDefaultAsync(c => c.Id == id);
            if (cd == null){ return NotFound(); }
            return mapper.Map<CDDTO>(cd);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] CDCreateDTO cDCreateDTO) 
        {
            var cd = mapper.Map<CD>(cDCreateDTO);
            /*
             if (cDCreateDTO.Poster != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await cDCreateDTO.Poster.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(cDCreateDTO.Poster.FileName);
                    cd.Poster = await fileUploader.saveFile(content, extension, container, cDCreateDTO.Poster.ContentType);
                }
            }
            context.Add(cd);
            await context.SaveChangesAsync();
            var cdDTO = mapper.Map<CDDTO>(cd);
            return new CreatedAtRouteResult("getCD",new { id = cd.Id }, cdDTO);
             */
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] CDCreateDTO cDCreateDTO)
        {
            var cdDB = await context.CDs.FirstOrDefaultAsync(x => x.Id == id);
            if (cdDB == null) { return NotFound(); }
            cdDB = mapper.Map(cDCreateDTO, cdDB);
            if (cDCreateDTO.Poster != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await cDCreateDTO.Poster.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(cDCreateDTO.Poster.FileName);
                    cdDB.Poster = await fileUploader.editFile(content, extension, container,
                        cdDB.Poster,
                        cDCreateDTO.Poster.ContentType);

                }
            }
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<CDPatchDTO> patchDocument)
        {
            if (patchDocument == null) { return BadRequest(); }
            var entityDB = await context.CDs.FirstOrDefaultAsync(x => x.Id == id);
            if (entityDB == null) { return NotFound(); }

            var entityDTO = mapper.Map<CDPatchDTO>(entityDB);
            patchDocument.ApplyTo(entityDTO, ModelState);
            var isValid = TryValidateModel(entityDB);

            if (!isValid) { return BadRequest(ModelState); }
            mapper.Map(entityDTO, entityDB);
            await context.SaveChangesAsync();
            return NoContent();

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await context.CDs.AnyAsync(x => x.Id == id);
            if (!exist) { return NotFound(); }
            context.Remove(new CD() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}
