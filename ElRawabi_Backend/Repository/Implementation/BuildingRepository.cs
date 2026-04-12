using ElRawabi_Backend.Data;
using ElRawabi_Backend.Models;
using ElRawabi_Backend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElRawabi_Backend.Repository.Implementation
{
    public class BuildingRepository : IBuildingRepository
    {
        private readonly ElRawabiDbContext _context;

        public BuildingRepository(ElRawabiDbContext context)
        {
            _context = context;
        }

        public async Task<List<Building>> GetAllAsync()
        {
            return await _context.Buildings
                .Include(b => b.Project)
                .Where(b => !b.IsDeleted)
                .ToListAsync();
        }

        public async Task<Building?> GetByIdAsync(int id)
        {
            return await _context.Buildings
                .Include(b => b.Project)
                // ✅ تم التعديل: جلب الأدوار والشقق التابعة لها
                .Include(b => b.Floors.Where(f => !f.IsDeleted))
                    .ThenInclude(f => f.Apartments.Where(a => !a.IsDeleted))
                .Include(b => b.BuildingImgs.Where(i => !i.IsDeleted))
                .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);
        }

        public async Task<List<Building>> GetByProjectIdAsync(int projectId)
        {
            return await _context.Buildings
                .Include(b => b.Project)
                .Include(b => b.Floors.Where(f => !f.IsDeleted))
                .Include(b => b.BuildingImgs.Where(i => !i.IsDeleted))
                .Where(b => b.ProjectId == projectId && !b.IsDeleted)
                .ToListAsync();
        }

        public async Task<Building?> GetByBuildingNumberAsync(string buildingNumber, int projectId)
        {
            return await _context.Buildings
                .FirstOrDefaultAsync(b => b.BuildingNumber == buildingNumber && b.ProjectId == projectId && !b.IsDeleted);
        }

        public async Task<Building> AddAsync(Building building)
        {
            await _context.Buildings.AddAsync(building);
            await _context.SaveChangesAsync();
            return building;
        }

        public async Task<Building> UpdateAsync(Building building)
        {
            _context.Buildings.Update(building);
            await _context.SaveChangesAsync();
            return building;
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Buildings.CountAsync(b => !b.IsDeleted);
        }
    }
}
