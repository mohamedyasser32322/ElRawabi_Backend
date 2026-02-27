using ElRawabi_Backend.Dtos.Buildings;
using ElRawabi_Backend.Dtos.Projects;

namespace ElRawabi_Backend.Services.Interface
{
    public interface IBuildingService
    {
        Task<List<AllBuildingsDto>> GetAllBuildingsAsync();
        Task<BuildingReadDto> GetBuildingByIdAsync(int id);
        Task<int> GetBuildingsCountAsync();
        Task<string> AddBuildingAsync(BuildingCreateDto buildingCreateDto);
        Task<List<AllBuildingsDto>> GetBuildingsByProjectIdAsync(int projectId);
        Task<BuildingReadDto> UpdateBuildingAsync(BuildingUpdateDto buildingUpdateDto);
        Task<bool> DeleteAsync(int id);
    }
}
