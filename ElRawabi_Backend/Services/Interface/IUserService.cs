using ElRawabi_Backend.Dtos.Users;

namespace ElRawabi_Backend.Services.Interface
{
    public interface IUserService
    {
        Task<List<AllUsersDto>> GetAllUsersAsync();
        Task<int> GetUsersCountAsync();
        Task<UserReadDto> GetByIdAsync(int id);
        Task<UserReadDto> UpdateUserAsync(UserUpdateDto userUpdateDto);
        Task<bool> DeleteAsync(int id);
    }
}
