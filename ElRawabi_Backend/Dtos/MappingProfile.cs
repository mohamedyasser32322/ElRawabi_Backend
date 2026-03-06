using AutoMapper;
using ElRawabi_Backend.Dtos.Apartment;
using ElRawabi_Backend.Dtos.Buildings;
using ElRawabi_Backend.Dtos.BuildingTimeLine;
using ElRawabi_Backend.Dtos.Interface_Dtos;
using ElRawabi_Backend.Dtos.Projects;
using ElRawabi_Backend.Dtos.Users;
using ElRawabi_Backend.Models;

namespace ElRawabi_Backend.Dtos
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Users Mapping
            CreateMap<User, AllUsersDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));
            CreateMap<User, UserReadDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));

            CreateMap<UserRegisterDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            CreateMap<User, AuthResponseDto>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));

            CreateMap<UserUpdateDto, User>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Projects Mapping
            CreateMap<Project, AllProjectsDto>();
            CreateMap<Project, ProjectReadDto>();
            CreateMap<ProjectCreateDto, Project>();
            CreateMap<ProjectUpdateDto, Project>()
                .ForAllMembers(opts => opts.Condition((src , dest , srcMember) => srcMember != null));

            // Buildings Mapping
            CreateMap<Building, AllBuildingsDto>();
            CreateMap<Building, BuildingReadDto>();
            CreateMap<BuildingCreateDto, Building>();
            CreateMap<BuildingUpdateDto, Building>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Apartments Mapping
            CreateMap<ElRawabi_Backend.Models.Apartment, AllApartmentsDto>();
            CreateMap<ElRawabi_Backend.Models.Apartment, ApartmentReadDto>();
            CreateMap<ApartmentCreateDto, ElRawabi_Backend.Models.Apartment>();
            CreateMap<ApartmentUpdateDto, ElRawabi_Backend.Models.Apartment>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Building TimeLine Mapping
            CreateMap<ElRawabi_Backend.Models.BuildingTimeLine, BuildingTimeLineReadDto>()
                .ForMember(dest => dest.StageName, opt => opt.MapFrom(src => src.Stage.ToString()))
                .ForMember(dest => dest.BuildingName, opt => opt.MapFrom(src => src.Building != null ? src.Building.BuildingNumber : "N/A"));
            CreateMap<BuildingTimeLineCreateDto, ElRawabi_Backend.Models.BuildingTimeLine>();
            CreateMap<BuildingTimeLineUpdateDto, ElRawabi_Backend.Models.BuildingTimeLine>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}