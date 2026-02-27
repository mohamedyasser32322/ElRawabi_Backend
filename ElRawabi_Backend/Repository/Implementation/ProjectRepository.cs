using ElRawabi_Backend.Data;
using ElRawabi_Backend.Models;
using ElRawabi_Backend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElRawabi_Backend.Repository.Implementation
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ElRawabiDbContext _context;
        public ProjectRepository(ElRawabiDbContext context)
        {
            _context = context;
        }

        public async Task<List<Project>> GetAllAsync()
        {
            return await _context.Projects.Where(p => !p.IsDeleted).ToListAsync();
        }

        public async Task<Project?> GetByIdAsync(int id)
        {
            return await _context.Projects
                .Include(p => p.Buildings)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        }

        public async Task<Project?> GetByProjectNameAsync(string name)
        {
            return await _context.Projects.FirstOrDefaultAsync(p => p.Name == name && !p.IsDeleted);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Projects.CountAsync(p => !p.IsDeleted);
        }

        public async Task<Project> AddAsync(Project Project)
        { 
            await _context.Projects.AddAsync(Project);
            await _context.SaveChangesAsync();
            return Project;
        }

        public async Task<Project> UpdateAsync(Project Project)
        {
            _context.Projects.Update(Project);
            await _context.SaveChangesAsync();
            return Project;
        }
    }
}
