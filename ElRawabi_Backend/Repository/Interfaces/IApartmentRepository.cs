using ElRawabi_Backend.Models;

namespace ElRawabi_Backend.Repository.Interfaces
{
    public interface IApartmentRepository
    {
        Task<List<Apartment>> GetAllAsync();
        Task<Apartment?> GetByIdAsync(int id);
        Task<List<Apartment>> GetApartmentsByFloorIdAsync(int floorId);
        Task<Apartment?> GetByApartmentNumberAndFloorIdAsync(string apartmentNumber, int floorId);
        Task<Apartment> AddAsync(Apartment apartment);
        Task<Apartment> UpdateAsync(Apartment apartment);
        Task<int> GetCountAsync();
        Task<List<Apartment>> GetApartmentsByClientIdAsync(int clientId);
        Task<List<Apartment>> GetApartmentsByUserEmailAsync(string email);
    }
}
