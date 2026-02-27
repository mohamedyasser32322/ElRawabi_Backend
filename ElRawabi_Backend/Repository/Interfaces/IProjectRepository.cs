using ElRawabi_Backend.Models;

namespace ElRawabi_Backend.Repository.Interfaces
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllAsync();
        Task<Project?> GetByIdAsync(int id);
        Task<Project?> GetByProjectNameAsync(string name);
        Task<int> GetCountAsync();
        Task<Project> AddAsync(Project Project);
        Task<Project> UpdateAsync(Project Project);
    }
}
