using ElRawabi_Backend.Models;

namespace ElRawabi_Backend.Repository.Interfaces
{
    public interface IFloorRepository
    {
        Task<List<Floor>> GetAllAsync();
        Task<Floor?> GetByIdAsync(int id);
        Task<List<Floor>> GetFloorsByBuildingIdAsync(int buildingId);
        Task<Floor?> GetByFloorNumberAndBuildingIdAsync(int floorNumber, int buildingId);
        Task<Floor> AddAsync(Floor floor);
        Task<Floor> UpdateAsync(Floor floor);
        Task<int> GetCountAsync();
    }
}
