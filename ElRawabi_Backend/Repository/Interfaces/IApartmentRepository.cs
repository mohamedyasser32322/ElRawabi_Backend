using ElRawabi_Backend.Models;

namespace ElRawabi_Backend.Repository.Interfaces
{
    public interface IApartmentRepository
    {
        Task<List<Apartment>> GetAllAsync();
        Task<Apartment?> GetByIdAsync(int id);
        Task<Apartment?> GetByApartmentNumberAsync(string apartmentNumber , int buildingId);
        Task<int> GetCountAsync();
        Task<Apartment> AddAsync(Apartment Apartment);
        Task<Apartment> UpdateAsync(Apartment Apartment);
        Task<Apartment?> GetApartmentWithDetailsAsync(int apartmentId);
    }
}
