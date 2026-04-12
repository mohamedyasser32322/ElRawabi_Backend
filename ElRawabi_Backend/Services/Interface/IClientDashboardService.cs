using ElRawabi_Backend.Dtos.Interface_Dtos;

namespace ElRawabi_Backend.Services.Interface
{
    public interface IClientDashboardService
    {
        Task<List<ClientHeaderDTO>> GetClientHeader(int clientId);
        Task<List<ClientHeaderDTO>> GetClientDashboardHeaderByEmailAsync(string email);
    }
}
