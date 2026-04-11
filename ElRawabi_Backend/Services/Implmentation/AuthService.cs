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
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AuthService(
            IUserRepository userRepository,
            JwtHelper jwtHelper,
            PasswordHelper passwordHelper,
            IMapper mapper,
            IEmailService emailService,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _jwtHelper = jwtHelper;
            _passwordHelper = passwordHelper;
            _mapper = mapper;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<string> RegisterAsync(UserRegisterDto registerDto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(registerDto.Email);
            if (existingUser != null) return "Email already exists";

            var user = _mapper.Map<User>(registerDto);

            user.PasswordHash = _passwordHelper.HashPassword(registerDto.Password);

            // الحساب هيكون غير مفعل افتراضياً بناءً على تعديل الـ Model
            user.IsActive = false;

            await _userRepository.AddAsync(user);
            return "Success";
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto loginDto)
        {
            var user = await _userRepository.GetByEmailAsync(loginDto.Email);

            if (user == null || !_passwordHelper.VerifyPassword(loginDto.Password, user.PasswordHash))
                return null;

            // التحقق من أن الحساب مفعل من قبل الإدارة
            if (!user.IsActive)
                throw new UnauthorizedAccessException("Account is not active. Please contact the administrator.");

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

        public async Task<bool> ForgotPasswordAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null) return false;

            // إنشاء Token فريد لاستعادة كلمة المرور
            var token = Guid.NewGuid().ToString();
            user.PasswordResetToken = token;
            user.ResetTokenExpires = DateTime.UtcNow.AddHours(1); // صالح لمدة ساعة

            await _userRepository.UpdateAsync(user);

            // إرسال رابط الاستعادة للإيميل
            var resetLink = $"{_configuration["AppUrl"]}/reset-password?token={token}";
            await _emailService.SendEmailAsync(
                user.Email!,
                "Reset Your Password",
                $"Click here to reset your password: <a href='{resetLink}'>Reset Password</a>"
            );

            return true;
        }

        public async Task<bool> ResetPasswordAsync(string token, string newPassword)
        {
            // البحث عن المستخدم بالـ Token والتأكد إنه مخلصش وقته
            var users = await _userRepository.GetAllAsync();
            var user = users.FirstOrDefault(u => u.PasswordResetToken == token && u.ResetTokenExpires > DateTime.UtcNow);

            if (user == null) return false;

            // تحديث كلمة المرور وتصفير الـ Token
            user.PasswordHash = _passwordHelper.HashPassword(newPassword);
            user.PasswordResetToken = null;
            user.ResetTokenExpires = null;

            await _userRepository.UpdateAsync(user);
            return true;
        }

        public async Task<bool> ActivateUserAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return false;

            // تفعيل الحساب يدوياً
            user.IsActive = true;
            await _userRepository.UpdateAsync(user);
            return true;
        }
    }

}
