using ElRawabi_Backend.Dtos.Apartment;
using ElRawabi_Backend.Dtos.Buildings;

namespace ElRawabi_Backend.Services.Interface
{
    public interface IApartmentService
    {
        Task<List<ApartmentReadDto>> GetAllApartmentsAsync();
        Task<ApartmentReadDto?> GetApartmentByIdAsync(int id);
        Task<List<ApartmentReadDto>> GetApartmentsByFloorIdAsync(int floorId);
        Task<string> AddApartmentAsync(ApartmentCreateDto dto);
        Task<ApartmentReadDto> UpdateApartmentAsync(ApartmentUpdateDto dto);
        Task<bool> DeleteApartmentAsync(int id);
        Task<int> GetApartmentsCountAsync();
    }
}
