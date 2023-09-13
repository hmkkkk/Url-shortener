using API.Dtos;
using API.Dtos.UserLink;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AppUser, UserDto>();
            CreateMap<UserLink, LinkDto>().ReverseMap();
            CreateMap<LinkAnonymousFormDto, UserLink>();
        }
    }
}