using AutoMapper;
using ElRawabi_Backend.Dtos.Interface_Dtos;
using ElRawabi_Backend.Repository.Interfaces;
using ElRawabi_Backend.Services.Interface;

namespace ElRawabi_Backend.Services.Implmentation
{
    public class ClientDashboardService : IClientDashboardService
    {
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IMapper _mapper;

        public ClientDashboardService(IApartmentRepository apartmentRepository, IMapper mapper)
        {
            _apartmentRepository = apartmentRepository;
            _mapper = mapper;
        }

        public async Task<List<ClientHeaderDTO>> GetClientHeader(int clientId)
        {
            var apartments = await _apartmentRepository.GetApartmentsByClientIdAsync(clientId);
            return _mapper.Map<List<ClientHeaderDTO>>(apartments);
        }

        public async Task<List<ClientHeaderDTO>> GetClientDashboardHeaderByEmailAsync(string email)
        {
            var apartments = await _apartmentRepository.GetApartmentsByUserEmailAsync(email);
            if (apartments == null || !apartments.Any()) return null;

            return _mapper.Map<List<ClientHeaderDTO>>(apartments);
        }

    }
}
