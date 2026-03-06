using AutoMapper;
using ElRawabi_Backend.Dtos.BuildingTimeLine;
using ElRawabi_Backend.Dtos.Interface_Dtos;
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

        public async Task<ClientHeaderDTO?> GetClientDashboardHeaderAsync(int apartmentId)
        {
            var apartment = await _apartmentRepository.GetApartmentWithDetailsAsync(apartmentId);

            if (apartment == null) return null;
            return new ClientHeaderDTO
            {
                ClientName = apartment.User?.FullName,
                ProjectName = apartment.Building?.Project?.Name,
                FloorNumber = apartment.FloorNumber.ToString(),
                UnitNumber = apartment.ApartmentNumber,
                DeliveryDate = apartment.Building?.DeliveryDate.ToString("yyyy-MM-dd"),
                AccountStatus = apartment.User?.IsActive,

                BuildingTimeLineReadDtos = apartment.Building?.buildingTimeLines?.Select(bt => new BuildingTimeLineReadDto
                {
                    Id = bt.Id,
                    StageName = bt.Stage.ToString(),
                    IsCompleted = bt.IsCompleted,
                    CompletionDate = bt.CompletedAt
                }).ToList()
            };
        }
    }
}
