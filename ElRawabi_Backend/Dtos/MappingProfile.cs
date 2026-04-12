using AutoMapper;
using ElRawabi_Backend.Dtos.Apartment;
using ElRawabi_Backend.Dtos.Buildings;
using ElRawabi_Backend.Dtos.BuildingTimeLine;
using ElRawabi_Backend.Dtos.Floor;
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
            // Floor Mapping
            CreateMap<Models.Floor, FloorReadDto>()
                .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Building != null ? src.Building.BuildingNumber : "غير محدد"))
                .ForMember(dest => dest.ApartmentCount, opt => opt.MapFrom(src => src.Apartments != null ? src.Apartments.Count(a => !a.IsDeleted) : 0));
            CreateMap<FloorCreateDto, Models.Floor>();
            CreateMap<FloorUpdateDto, Models.Floor>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

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
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Buildings Mapping
            CreateMap<Building, AllBuildingsDto>()
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project != null ? src.Project.Name : "غير محدد"));
            CreateMap<Building, BuildingReadDto>()
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project != null ? src.Project.Name : "غير محدد"))
                .ForMember(dest => dest.Floors, opt => opt.MapFrom(src => src.Floors != null ? src.Floors.Where(f => !f.IsDeleted).ToList() : new List<Models.Floor>()))
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.BuildingImgs != null ? src.BuildingImgs.Where(i => !i.IsDeleted).Select(i => i.ImgUrl).ToList() : new List<string>()));
            CreateMap<BuildingCreateDto, Building>();
            CreateMap<BuildingUpdateDto, Building>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Apartments Mapping
            CreateMap<Models.Apartment, AllApartmentsDto>();
            CreateMap<Models.Apartment, ApartmentReadDto>()
                .ForMember(dest => dest.FloorNumber, opt => opt.MapFrom(src => src.Floor != null ? src.Floor.FloorNumber : 0))
                .ForMember(dest => dest.BuildingId, opt => opt.MapFrom(src => src.Floor != null ? src.Floor.BuildingId : 0))
                .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Floor != null && src.Floor.Building != null ? src.Floor.Building.BuildingNumber : "غير محدد"))
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.Floor != null && src.Floor.Building != null ? src.Floor.Building.ProjectId : 0))
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Floor != null && src.Floor.Building != null && src.Floor.Building.Project != null ? src.Floor.Building.Project.Name : "غير محدد"))
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.Type == ApartmentType.GroundFloor ? "أرضي" : src.Type == ApartmentType.TypicalFloor ? "متكرر" : "روف"))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src =>
                    src.Status == ApartmentStatus.Available ? "متاح" :
                    src.Status == ApartmentStatus.Reserved ? "محجوز" :
                    src.Status == ApartmentStatus.Sold ? "مباع" :
                    src.Status == ApartmentStatus.Closed ? "مقفول" : "غير محدد"))
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client != null ? src.Client.FullName : "بدون عميل"));

            CreateMap<ApartmentCreateDto, Models.Apartment>();
            CreateMap<ApartmentUpdateDto, Models.Apartment>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Client Dashboard Header Mapping
            CreateMap<Models.Apartment, ClientHeaderDTO>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client != null ? src.Client.FullName : "بدون عميل"))
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Floor != null && src.Floor.Building != null && src.Floor.Building.Project != null ? src.Floor.Building.Project.Name : "غير محدد"))
                .ForMember(dest => dest.UnitNumber, opt => opt.MapFrom(src => src.ApartmentNumber))
                .ForMember(dest => dest.FloorNumber, opt => opt.MapFrom(src => src.Floor != null ? src.Floor.FloorNumber.ToString() : "0"))
                .ForMember(dest => dest.AccountStatus, opt => opt.MapFrom(src =>
                    src.Status == ApartmentStatus.Available ? "متاح" :
                    src.Status == ApartmentStatus.Reserved ? "محجوز" :
                    src.Status == ApartmentStatus.Sold ? "مباع" :
                    src.Status == ApartmentStatus.Closed ? "مقفول" : "غير محدد"))
                .ForMember(dest => dest.DeliveryDate, opt => opt.MapFrom(src => src.Floor != null && src.Floor.Building != null ? src.Floor.Building.DeliveryDate.ToString("yyyy/MM/dd") : "غير محدد"))
                .ForMember(dest => dest.BuildingTimeLineReadDtos, opt => opt.Ignore());

            // Building TimeLine Mapping
            CreateMap<Models.BuildingTimeLine, BuildingTimeLineReadDto>()
                .ForMember(dest => dest.StageNumber, opt => opt.MapFrom(src => (int)src.Stage))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.IsCompleted == true ? "active" : "current"))
                .ForMember(dest => dest.DateText, opt => opt.MapFrom(src => src.IsCompleted == true && src.CompletedAt.HasValue ? src.CompletedAt.Value.ToString("yyyy/MM/dd") : "قيد التنفيذ"))
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
