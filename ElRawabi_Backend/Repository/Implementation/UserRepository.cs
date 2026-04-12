using ElRawabi_Backend.Data;
using ElRawabi_Backend.Models;
using ElRawabi_Backend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElRawabi_Backend.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ElRawabiDbContext _context;
        public UserRepository(ElRawabiDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.Where(u => !u.IsDeleted).ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Users.CountAsync();
        }
        public async Task<User> AddAsync(User User)
        {
            await _context.Users.AddAsync(User);
            await _context.SaveChangesAsync();
            return User;
        }

        public async Task<User> UpdateAsync(User User)
        {
            _context.Users.Update(User);
            await _context.SaveChangesAsync();
            return User;
        }

        public async Task<User?> GetByResetTokenAsync(string token)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == token && !u.IsDeleted);
        }
    }
}
