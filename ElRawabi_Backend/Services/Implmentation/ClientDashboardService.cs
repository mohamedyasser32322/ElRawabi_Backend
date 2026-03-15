using AutoMapper;
using ElRawabi_Backend.Dtos.BuildingTimeLine;
using ElRawabi_Backend.Dtos.Interface_Dtos;
using ElRawabi_Backend.Models;
using ElRawabi_Backend.Repository.Interfaces;
using ElRawabi_Backend.Services.Interface;

namespace ElRawabi_Backend.Services.Implmentation
{
    public class ClientDashboardService : IClientDashboardService
    {
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IMapper _mapper;
        public ClientDashboardService(IApartmentRepository apartmentRepository,IMapper mapper)
        {
            _apartmentRepository = apartmentRepository;
            _mapper = mapper;
        }

        public async Task<ClientHeaderDTO?> GetClientDashboardHeaderByEmailAsync(string email)
        {
            var apartment = await _apartmentRepository.GetApartmentByEmailAsync(email);
            if (apartment == null) return null;

            string[] stageNamesAr = {
        "توقيع العقود", "المخططات والتراخيص", "الأساسات",
        "الهيكل الإنشائي", "اللياسة والكهرباء", "التشطيبات النهائية", "الاستلام والتشغيل"
        };

            var timelineDtos = new List<BuildingTimeLineReadDto>();
            var dbStages = apartment.Building?.buildingTimeLines?.ToList() ?? new List<BuildingTimeLine>();
            bool foundCurrent = false;

            for (int i = 1; i <= 7; i++)
            {
                var stageData = dbStages.FirstOrDefault(s => (int)s.Stage == i);
                string status = "disabled";
                string dateText = "--";

                if (stageData != null)
                {
                    if (stageData.IsCompleted == true)
                    {
                        status = "active";
                        dateText = stageData.CompletedAt?.ToString("yyyy/MM/dd") ?? "--";
                    }
                    else if (!foundCurrent)
                    {
                        status = "current";
                        dateText = "جاري التنفيذ";
                        foundCurrent = true;
                    }
                }
                else if (!foundCurrent)
                {
                    status = "current";
                    dateText = "قيد الانتظار";
                    foundCurrent = true;
                }

                timelineDtos.Add(new BuildingTimeLineReadDto
                {
                    StageNumber = i,
                    StageDisplayName = stageNamesAr[i - 1],
                    Status = status,
                    DateText = dateText
                });
            }

            return new ClientHeaderDTO
            {
                ClientName = apartment.User?.FullName,
                ProjectName = apartment.Building?.Project?.Name,
                FloorNumber = apartment.FloorNumber.ToString(),
                UnitNumber = "شقة " + apartment.ApartmentNumber,
                DeliveryDate = apartment.Building?.DeliveryDate.ToString("yyyy/MM/dd"),
                AccountStatus = apartment.User?.IsActive ?? false,
                BuildingTimeLineReadDtos = timelineDtos
            };
        }
    }
}