using ElRawabi_Backend.Data;
using ElRawabi_Backend.Repository.Interfaces;
using ElRawabi_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace ElRawabi_Backend.Repository.Implementation
{
    public class BuildingImgRepository : IBuildingImgRepository
    {
        private readonly ElRawabiDbContext _context;
        public BuildingImgRepository(ElRawabiDbContext context)
        {
            _context = context;
        }

        public async Task<List<BuildingImg>> GetAllAsync()
        {
            return await _context.BuildingImgs.Where(bi => !bi.IsDeleted).ToListAsync();
        }

        public async Task<BuildingImg?> GetByIdAsync(int id)
        {
            return await _context.BuildingImgs.FirstOrDefaultAsync(bi => bi.Id == id);
        }


        public async Task<BuildingImg> AddAsync(BuildingImg BuildingImg)
        {
            await _context.BuildingImgs.AddAsync(BuildingImg);
            await _context.SaveChangesAsync();
            return BuildingImg;
        }

        public async Task<BuildingImg> UpdateAsync(BuildingImg BuildingImg)
        {
            _context.BuildingImgs.Update(BuildingImg);
            await _context.SaveChangesAsync();
            return BuildingImg;
        }
    }
}
