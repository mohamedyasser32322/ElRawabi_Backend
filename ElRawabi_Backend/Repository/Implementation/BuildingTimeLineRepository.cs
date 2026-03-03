using ElRawabi_Backend.Data;
using ElRawabi_Backend.Models;
using ElRawabi_Backend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElRawabi_Backend.Repository.Implementation
{
    public class BuildingTimeLineRepository : IBuildingTimeLineRepository
    {
        private readonly ElRawabiDbContext _context;
        public BuildingTimeLineRepository(ElRawabiDbContext context)
        {
            _context = context;
        }

        public async Task<List<BuildingTimeLine>> GetAllAsync()
        {
            return await _context.BuildingsTimeLine.Where(bt => !bt.IsDeleted).ToListAsync();
        }

        public async Task<BuildingTimeLine?> GetByIdAsync(int id)
        { 
            return await _context.BuildingsTimeLine.FirstOrDefaultAsync(bt => bt.Id == id && !bt.IsDeleted);
        }

        public async Task<BuildingTimeLine> AddAsync(BuildingTimeLine BuildingTimeLine)
        {
            await _context.BuildingsTimeLine.AddAsync(BuildingTimeLine);
            await _context.SaveChangesAsync();
            return BuildingTimeLine;
        }

        public async Task<BuildingTimeLine> UpdateAsync(BuildingTimeLine BuildingTimeLine)
        {
            _context.BuildingsTimeLine.Update(BuildingTimeLine);
            await _context.SaveChangesAsync();
            return BuildingTimeLine;
        }
    }
}
