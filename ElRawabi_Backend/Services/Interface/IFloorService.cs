using ElRawabi_Backend.Dtos.Floor;

namespace ElRawabi_Backend.Services.Interface
{
    public interface IFloorService
    {
        Task<List<FloorReadDto>> GetFloorsByBuildingIdAsync(int buildingId);
        Task<FloorReadDto?> GetFloorByIdAsync(int id);
        Task<string> AddFloorAsync(FloorCreateDto dto);
        Task<FloorReadDto> UpdateFloorAsync(FloorUpdateDto dto);
        Task<bool> DeleteFloorAsync(int id);
    }
}
