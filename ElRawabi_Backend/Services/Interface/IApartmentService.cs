using ElRawabi_Backend.Dtos.Apartment;
using ElRawabi_Backend.Dtos.Buildings;

namespace ElRawabi_Backend.Services.Interface
{
    public interface IApartmentService
    {
        Task<List<AllApartmentsDto>> GetAllApartmentsAsync();
        Task<ApartmentReadDto> GetApartmentByIdAsync(int id);
        Task<int> GetApartmentsCountAsync();
        Task<string> AddApartmentAsync(ApartmentCreateDto  apartmentCreateDto);
        Task<ApartmentReadDto> UpdateApartmentAsync(ApartmentUpdateDto apartmentUpdateDto);
        Task<bool> DeleteAsync(int id);
    }
}
