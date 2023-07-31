using api_music.DTOs.GenderDTOs;
using api_music.DTOs.MemberDTOs;
using api_music.Entities;
using AutoMapper;

namespace api_music.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Gender,GenderDTO>().ReverseMap();
            CreateMap<GenderCreateDTO,Gender>();

            CreateMap<Member, MemberDTO>().ReverseMap();
            CreateMap<MemberCreateDTO, Member>().ForMember(x=>x.Avatar, options =>options.Ignore());

            CreateMap<MemberPatchDTO, Member>().ReverseMap();
        }
    }
}
