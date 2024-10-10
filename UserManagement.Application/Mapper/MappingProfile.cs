using AutoMapper;
using UserManagement.Application.DTOs;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, ApplicationUser>().ReverseMap();
            CreateMap<RegistrationDto, ApplicationUser>().ReverseMap();
            CreateMap<RegistrationDto, UserProfileDto>().ReverseMap();
            CreateMap<ApplicationUser, UserProfileDto>().ReverseMap();

            CreateMap<ApplicationRole, RoleDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Name))
                .ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.RoleName));

        }
    }
}
