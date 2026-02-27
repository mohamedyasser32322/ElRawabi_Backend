using ElRawabi_Backend.Models;

namespace ElRawabi_Backend.Repository.Interfaces
{
    public interface IBuildingTimeLineRepository
    {
        Task<List<BuildingTimeLine>> GetAllAsync();
        Task<BuildingTimeLine?> GetByIdAsync(int id);
        Task<BuildingTimeLine> AddAsync(BuildingTimeLine BuildingTimeLine);
        Task<BuildingTimeLine> UpdateAsync(BuildingTimeLine BuildingTimeLine);
    }
}
