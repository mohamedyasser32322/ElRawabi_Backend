using ElRawabi_Backend.Models;

namespace ElRawabi_Backend.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<int> GetCountAsync();
        Task<User> AddAsync(User User);
        Task<User> UpdateAsync(User User);
    }
}
