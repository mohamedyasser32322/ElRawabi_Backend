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
            return await _context.Apartments
                .Include(a => a.Client)
                .Include(a => a.Floor)
                    .ThenInclude(f => f.Building)
                        .ThenInclude(b => b.Project)
                .Where(a => !a.IsDeleted)
                .ToListAsync();
        }

        public async Task<Apartment?> GetByIdAsync(int id)
        {
            return await _context.Apartments
                .Include(a => a.Client)
                .Include(a => a.Floor)
                    .ThenInclude(f => f.Building)
                        .ThenInclude(b => b.Project)
                .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
        }
        public async Task<List<Apartment>> GetApartmentsByFloorIdAsync(int floorId)
        {
            return await _context.Apartments
                .Include(a => a.Client)
                .Where(a => a.FloorId == floorId && !a.IsDeleted)
                .ToListAsync();
        }

        // ✅ تم التعديل: التحقق من تكرار الشقة في نفس الدور
        public async Task<Apartment?> GetByApartmentNumberAndFloorIdAsync(string apartmentNumber, int floorId)
        {
            return await _context.Apartments
                .FirstOrDefaultAsync(a => a.ApartmentNumber == apartmentNumber && a.FloorId == floorId && !a.IsDeleted);
        }

        public async Task<Apartment> AddAsync(Apartment apartment)
        {
            await _context.Apartments.AddAsync(apartment);
            await _context.SaveChangesAsync();
            return apartment;
        }

        public async Task<Apartment> UpdateAsync(Apartment apartment)
        {
            _context.Apartments.Update(apartment);
            await _context.SaveChangesAsync();
            return apartment;
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Apartments.CountAsync(a => !a.IsDeleted);
        }

        // ✅ جديد: جلب شقق عميل معين للداشبورد مع كل البيانات المرتبطة
        public async Task<List<Apartment>> GetApartmentsByClientIdAsync(int clientId)
        {
            return await _context.Apartments
                .Where(a => a.ClientId == clientId && !a.IsDeleted)
                .Include(a => a.Client)
                .Include(a => a.Floor)
                .ThenInclude(f => f.Building)
                .ThenInclude(b => b.Project)
                .Include(a => a.Floor)
                .ThenInclude(f => f.Building)
                .ThenInclude(b => b.buildingTimeLines)
                .ToListAsync();
        }

        public async Task<List<Apartment>> GetApartmentsByUserEmailAsync(string email)
        {
            return await _context.Apartments
                .Where(a => a.Client.Email == email && !a.IsDeleted)
                .Include(a => a.Client)
                .Include(a => a.Floor)
                .ThenInclude(f => f.Building)
                .ThenInclude(b => b.Project)
                .Include(a => a.Floor)
                .ThenInclude(f => f.Building)
                .ThenInclude(b => b.buildingTimeLines)
                .ToListAsync();
        }
    }
}
