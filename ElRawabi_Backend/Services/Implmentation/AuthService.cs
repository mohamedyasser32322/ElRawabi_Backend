using AutoMapper;
using ElRawabi_Backend.Dtos.Users;
using ElRawabi_Backend.Helpers;
using ElRawabi_Backend.Models;
using ElRawabi_Backend.Repository.Interfaces;
using ElRawabi_Backend.Services.Interface;

namespace ElRawabi_Backend.Services.Implmentation
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtHelper _jwtHelper;
        private readonly PasswordHelper _passwordHelper;
        private readonly IMapper _mapper;
        public AuthService(IUserRepository userRepository,JwtHelper jwtHelper, PasswordHelper passwordHelper ,IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtHelper = jwtHelper;
            _passwordHelper = passwordHelper;
            _mapper = mapper;
        }

        public async Task<string> RegisterAsync(UserRegisterDto registerDto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(registerDto.Email);
            if (existingUser != null) return "Email already exists";

            var user = _mapper.Map<User>(registerDto);

            user.PasswordHash = _passwordHelper.HashPassword(registerDto.Password);

            await _userRepository.AddAsync(user);
            return "Success";
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto loginDto)
        {
            var user = await _userRepository.GetByEmailAsync(loginDto.Email);

            if (user == null || !_passwordHelper.VerifyPassword(loginDto.Password, user.PasswordHash))
                return null;

            var response = _mapper.Map<AuthResponseDto>(user);
            response.Token = _jwtHelper.GenerateToken(user);

            return response;
        }

        public async Task<bool> ChangePasswordAsync(int userId, UserChangePasswordDto changePasswordDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return false;

            if (!_passwordHelper.VerifyPassword(changePasswordDto.CurrentPassword, user.PasswordHash))
                return false;

            user.PasswordHash = _passwordHelper.HashPassword(changePasswordDto.NewPassword);
            user.LastUpdatedAt = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);
            return true;
        }
    }
}
