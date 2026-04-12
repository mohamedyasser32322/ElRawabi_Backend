using ElRawabi_Backend.Dtos.Buildings;
using ElRawabi_Backend.Dtos.Projects;
using ElRawabi_Backend.Models;

namespace ElRawabi_Backend.Services.Interface
{
    public interface IBuildingService
    {
        Task<List<AllBuildingsDto>> GetAllBuildingsAsync();
        Task<BuildingReadDto> GetBuildingByIdAsync(int id);
        Task<int> GetBuildingsCountAsync();
        Task<string> AddBuildingAsync(BuildingCreateDto dto, List<IFormFile>? images = null);
        Task<List<AllBuildingsDto>> GetBuildingsByProjectIdAsync(int projectId);
        Task<BuildingReadDto> UpdateBuildingAsync(BuildingUpdateDto dto, List<IFormFile>? newImages = null);
        Task<bool> DeleteAsync(int id);
        Task<BuildingImg?> GetBuildingImageByIdAsync(int imageId);
        Task<bool> DeleteBuildingImageAsync(int imageId);
    }
}
