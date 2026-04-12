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

            // إنشاء Token فريد
            var token = Guid.NewGuid().ToString();
            user.PasswordResetToken = token;
            user.ResetTokenExpires = DateTime.UtcNow.AddHours(1);

            await _userRepository.UpdateAsync(user);

            // بناء الرابط الصحيح (تأكد من وجود .html)
            var resetLink = $"{_configuration["AppUrl"]}/ResetPassword.html?token={token}";

            // نص الإيميل بتنسيق HTML شيك
            var emailBody = $@"
        <div style='font-family: Arial, sans-serif; direction: rtl; text-align: right; padding: 20px; border: 1px solid #eee; border-radius: 10px;'>
            <h2 style='color: #2c3e50;'>استعادة كلمة المرور</h2>
            <p>أهلاً بك، لقد تلقينا طلباً لإعادة تعيين كلمة المرور الخاصة بحسابك.</p>
            <p>يرجى الضغط على الزر أدناه لتغيير كلمة المرور (الرابط صالح لمدة ساعة واحدة)</p>
            <div style='text-align: center; margin: 30px 0;'>
                <a href='{resetLink}' style='background-color: #27ae60; color: white; padding: 12px 25px; text-decoration: none; border-radius: 5px; font-weight: bold;'>تغيير كلمة المرور</a>
            </div>
            <p style='color: #7f8c8d; font-size: 12px;'>إذا لم تطلب هذا التغيير، يمكنك تجاهل هذا الإيميل بأمان.</p>
            <hr style='border: 0; border-top: 1px solid #eee;'>
            <p style='font-size: 12px; color: #bdc3c7;'>فريق عمل الروابي</p>
        </div>";

            await _emailService.SendEmailAsync(
                user.Email!,
                "استعادة كلمة المرور - الروابي",
                emailBody
            );

            return true;
        }


        public async Task<bool> ResetPasswordAsync(string token, string newPassword)
        {
            // البحث المباشر عن المستخدم بالتوكن والتأكد من عدم انتهاء الصلاحية
            var user = await _userRepository.GetByResetTokenAsync(token);

            if (user == null || user.ResetTokenExpires < DateTime.UtcNow) return false;

            // تحديث كلمة المرور وتصفير التوكن
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
