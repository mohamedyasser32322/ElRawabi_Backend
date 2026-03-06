using ElRawabi_Backend.Dtos.Interface_Dtos;

namespace ElRawabi_Backend.Services.Interface
{
    public interface IClientDashboardService
    {
        Task<ClientHeaderDTO?> GetClientDashboardHeaderAsync(int apartmentId);
    }
}
