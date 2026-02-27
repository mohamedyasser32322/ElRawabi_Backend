using ElRawabi_Backend.Models;

namespace ElRawabi_Backend.Repository.Interfaces
{
    public interface IBuildingRepository
    {
        Task<List<Building>> GetAllAsync();
        Task<Building?> GetByIdAsync(int id);
        Task<List<Building>> GetByProjectIdAsync(int projectId);
        Task<Building?> GetByBuildingNumberAsync(string buildingNumber, int projectId);
        Task<int> GetCountAsync();
        Task<Building>AddAsync(Building Building);
        Task<Building> UpdateAsync(Building Building);
    }
}
