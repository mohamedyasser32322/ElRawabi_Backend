using ElRawabi_Backend.Data;
using ElRawabi_Backend.Models;
using ElRawabi_Backend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElRawabi_Backend.Repository.Implementation
{
    public class ApartmentRepository : IApartmentRepository
    {
        private readonly ElRawabiDbContext _context;
        public ApartmentRepository(ElRawabiDbContext context)
        {
            _context = context;
        }

        public async Task<List<Apartment>> GetAllAsync()
        {
            return await _context.Apartments.Where(a => !a.IsDeleted).ToListAsync();
        }

        public async Task<Apartment?> GetByIdAsync(int id)
        {
            return await _context.Apartments.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Apartment?> GetByApartmentNumberAsync(string apartmentNumber ,int buildingId)
        {
            return await _context.Apartments.FirstOrDefaultAsync(a => a.ApartmentNumber == apartmentNumber
            && a.BuildingId == buildingId
            && !a.IsDeleted);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Apartments.CountAsync(a => !a.IsDeleted);
        }

        public async Task<Apartment> AddAsync(Apartment Apartment) 
        {
            await _context.Apartments.AddAsync(Apartment);
            await _context.SaveChangesAsync();
            return Apartment;
        }

        public async Task<Apartment> UpdateAsync(Apartment Apartment)
        {
            _context.Apartments.Update(Apartment);
            await _context.SaveChangesAsync();
            return Apartment;
        }
    }
}
