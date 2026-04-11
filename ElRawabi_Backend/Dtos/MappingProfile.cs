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
            CreateMap<Building, AllBuildingsDto>()
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.Name));
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
            CreateMap<ElRawabi_Backend.Models.Apartment, ClientHeaderDTO>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.FullName))
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Building.Project.Name))
                .ForMember(dest => dest.UnitNumber, opt => opt.MapFrom(src => src.ApartmentNumber))
                .ForMember(dest => dest.FloorNumber, opt => opt.MapFrom(src => src.FloorNumber.ToString()))
                .ForMember(dest => dest.AccountStatus, opt => opt.MapFrom(src => src.Client.IsActive))
                .ForMember(dest => dest.DeliveryDate, opt => opt.MapFrom(src => src.Building.DeliveryDate.ToString("yyyy/MM/dd")))
                .ForMember(dest => dest.BuildingTimeLineReadDtos, opt => opt.Ignore());

            


        CreateMap<Models.BuildingTimeLine, BuildingTimeLineReadDto>()
                .ForMember(dest => dest.StageNumber, opt => opt.MapFrom(src => (int)src.Stage))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.IsCompleted == true ? "active" : "current"))
                .ForMember(dest => dest.DateText, opt => opt.MapFrom(src => src.IsCompleted == true && src.CompletedAt.HasValue? src.CompletedAt.Value.ToString("yyyy/MM/dd"): "قيد التنفيذ"))
                .ForMember(dest => dest.StageDisplayName, opt => opt.MapFrom(src => GetArabicStageName(src.Stage)));
                      CreateMap<BuildingTimeLineCreateDto, Models.BuildingTimeLine>();

            CreateMap<BuildingTimeLineUpdateDto, Models.BuildingTimeLine>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
        private string GetArabicStageName(BuildingStage stage)
        {
            return stage switch
            {
                BuildingStage.Stage1 => "توقيع العقود",
                BuildingStage.Stage2 => "المخططات والتراخيص",
                BuildingStage.Stage3 => "الأساسات",
                BuildingStage.Stage4 => "الهيكل الإنشائي",
                BuildingStage.Stage5 => "اللياسة والكهرباء",
                BuildingStage.Stage6 => "التشطيبات النهائية",
                BuildingStage.Stage7 => "الاستلام",
                _ => "مرحلة غير معروفة"
            };
        }
    }
}