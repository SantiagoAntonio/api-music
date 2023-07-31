using api_music.DTOs;
using api_music.DTOs.MemberDTOs;
using api_music.Entities;
using api_music.Helpers;
using api_music.Services;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace api_music.Controllers
{
    [ApiController]
    [Route("api/members")]
    public class MemberController:ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IFileUploader fileUploader;
        private readonly string container = "members";

        public MemberController( ApplicationDbContext context, IMapper mapper, IFileUploader fileUploader)
        {
            this.context = context;
            this.mapper = mapper;
            this.fileUploader = fileUploader;
        }

        [HttpGet]
        public async Task<ActionResult<List<MemberDTO>>> Get([FromQuery] PageDTO pageDTO)
        {
            var queryable = context.Members.AsQueryable();
            await HttpContext.InsertParamsPage(queryable, pageDTO.CantRegPerPage);

            var entidades = await queryable.Pager(pageDTO).ToListAsync();
            return mapper.Map<List<MemberDTO>>(entidades);
        }
        [HttpGet("{id}", Name = "getMember")]
        public async Task<ActionResult<MemberDTO>> Get(int id)
        {
            var entity = await context.Members.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) { return NotFound(); }
            return mapper.Map<MemberDTO>(entity);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] MemberCreateDTO memberCreateDTO)
        {
            var entity = mapper.Map<Member>(memberCreateDTO);

            if (memberCreateDTO.Avatar != null) 
            {
                using (var memoryStream = new MemoryStream())
                {
                    await memberCreateDTO.Avatar.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(memberCreateDTO.Avatar.FileName);
                    entity.Avatar = await fileUploader.saveFile(content, extension, container, memberCreateDTO.Avatar.ContentType);
                    
                }
            }

            context.Add(entity);
            await context.SaveChangesAsync();

            var memberDto = mapper.Map<MemberDTO>(entity);
            return new CreatedAtRouteResult("getMember", new {id=entity.Id}, memberDto );

        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] MemberCreateDTO memberCreateDTO)
        {
            var memberDB = await context.Members.FirstOrDefaultAsync(x => x.Id == id);
            if (memberDB == null){ return NotFound(); }
            memberDB = mapper.Map(memberCreateDTO, memberDB);
            if (memberCreateDTO.Avatar != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await memberCreateDTO.Avatar.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(memberCreateDTO.Avatar.FileName);
                    memberDB.Avatar = await fileUploader.editFile(content, extension, container,
                        memberDB.Avatar,
                        memberCreateDTO.Avatar.ContentType);

                }
            }
            await context.SaveChangesAsync();
            return NoContent();


            /*
            var entity = mapper.Map<Member>(memberCreateDTO);
            entity.Id = id;
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent(); 
            */
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<MemberPatchDTO> patchDocument)
        {
            if ( patchDocument == null ) { return BadRequest(); }
            var entityDB = await context.Members.FirstOrDefaultAsync(x=>x.Id == id);
            if ( entityDB == null) {  return NotFound(); }

            var entityDTO = mapper.Map<MemberPatchDTO>(entityDB);
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
            var exist = await context.Members.AnyAsync(x=>x.Id == id);
            if (!exist) { return NotFound(); }
            context.Remove(new Member() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }




    }
}
