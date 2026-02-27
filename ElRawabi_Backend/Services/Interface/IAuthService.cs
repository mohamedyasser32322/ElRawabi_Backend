using ElRawabi_Backend.Dtos.Users;

namespace ElRawabi_Backend.Services.Interface
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(UserRegisterDto registerDto);
        Task<AuthResponseDto?> LoginAsync(LoginRequestDto loginDto);
        Task<bool> ChangePasswordAsync(int userId, UserChangePasswordDto changePasswordDto);
    }
}
