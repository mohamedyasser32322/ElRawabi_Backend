using ElRawabi_Backend.Data;
using ElRawabi_Backend.Repository.Interfaces;
using ElRawabi_Backend.Models;
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
                .Where(b => !b.IsDeleted)
                .Include(b => b.Project)
                .ToListAsync();
        }

        public async Task<Building?> GetByIdAsync(int id)
        { 
            return await _context.Buildings
                .Include(b => b.Apartments)
                .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);
        }

        public async Task<List<Building>> GetByProjectIdAsync(int projectId)
        {
            return await _context.Buildings
                .Where(b => b.ProjectId == projectId && !b.IsDeleted)
                .Include(b => b.Project)
                .ToListAsync();
        }

        public async Task<Building?> GetByBuildingNumberAsync(string buildingNumber , int projectId)
        {
            return await _context.Buildings.FirstOrDefaultAsync(b => b.BuildingNumber == buildingNumber
                               && b.ProjectId == projectId
                               && !b.IsDeleted);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Buildings.CountAsync(b => !b.IsDeleted);
        }

        public async Task<Building> AddAsync(Building Building)
        { 
            await _context.Buildings.AddAsync(Building);
            await _context.SaveChangesAsync();
            return Building;
        }

        public async Task<Building> UpdateAsync(Building Building)
        {
            _context.Buildings.Update(Building);
            await _context.SaveChangesAsync();
            return Building;
        }
    }
}
