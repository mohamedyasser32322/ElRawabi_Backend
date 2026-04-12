using ElRawabi_Backend.Data;
using ElRawabi_Backend.Models;
using ElRawabi_Backend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElRawabi_Backend.Repository.Implementation
{
    public class FloorRepository : IFloorRepository
    {
        private readonly ElRawabiDbContext _context;

        public FloorRepository(ElRawabiDbContext context)
        {
            _context = context;
        }

        public async Task<List<Floor>> GetAllAsync()
        {
            return await _context.Floors
                .Include(f => f.Building)
                .Where(f => !f.IsDeleted)
                .ToListAsync();
        }

        public async Task<Floor?> GetByIdAsync(int id)
        {
            return await _context.Floors
                .Include(f => f.Building)
                .Include(f => f.Apartments.Where(a => !a.IsDeleted))
                    .ThenInclude(a => a.Client)
                .FirstOrDefaultAsync(f => f.Id == id && !f.IsDeleted);
        }

        public async Task<List<Floor>> GetFloorsByBuildingIdAsync(int buildingId)
        {
            return await _context.Floors
                .Include(f => f.Building)
                .Include(f => f.Apartments.Where(a => !a.IsDeleted))
                .Where(f => f.BuildingId == buildingId && !f.IsDeleted)
                .OrderBy(f => f.FloorNumber)
                .ToListAsync();
        }

        public async Task<Floor?> GetByFloorNumberAndBuildingIdAsync(int floorNumber, int buildingId)
        {
            return await _context.Floors
                .FirstOrDefaultAsync(f => f.FloorNumber == floorNumber && f.BuildingId == buildingId && !f.IsDeleted);
        }

        public async Task<Floor> AddAsync(Floor floor)
        {
            await _context.Floors.AddAsync(floor);
            await _context.SaveChangesAsync();
            return floor;
        }

        public async Task<Floor> UpdateAsync(Floor floor)
        {
            _context.Floors.Update(floor);
            await _context.SaveChangesAsync();
            return floor;
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Floors.CountAsync(f => !f.IsDeleted);
        }
    }
}
