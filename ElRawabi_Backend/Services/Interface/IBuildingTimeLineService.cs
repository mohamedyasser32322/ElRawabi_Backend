using ElRawabi_Backend.Dtos.BuildingTimeLine;

namespace ElRawabi_Backend.Services.Interface
{
    public interface IBuildingTimeLineService
    {
        Task<BuildingTimeLineReadDto> GetBuildingTimeLineByIdAsync(int id);
        Task<string> AddBuildingTimeLineAsync(BuildingTimeLineCreateDto buildingTimeLineCreateDto);
        Task<BuildingTimeLineReadDto> UpdateBuildingTimeLineAsync(BuildingTimeLineUpdateDto buildingTimeLineUpdateDto);
        Task<bool> DeleteBuildingTimeLineAsync(int id);
        Task<List<BuildingTimeLineReadDto>> GetBuildingTimeLinesByBuildingIdAsync(int buildingId);
    }
}
