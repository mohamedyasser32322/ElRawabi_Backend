using ElRawabi_Backend.Dtos.Users;
using ElRawabi_Backend.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ElRawabi_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            try
            {
                var response = await _authService.LoginAsync(loginDto);
                if (response == null)
                    return Unauthorized(new { message = "Invalid email or password" });

                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] UserChangePasswordDto changePasswordDto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();

            var userId = int.Parse(userIdClaim.Value);
            var result = await _authService.ChangePasswordAsync(userId, changePasswordDto);

            if (!result)
                return BadRequest(new { message = "Current password is wrong or user not found" });

            return Ok(new { message = "Password updated successfully" });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto);

            if (result == "Email already exists")
                return BadRequest(new { message = result });

            return Ok(new { message = "User registered successfully. Please wait for administrator activation." });
        }

        [HttpPost("forgot-password")]
public async Task<IActionResult> ForgotPassword([FromBody] string email)
{
    try {
        var result = await _authService.ForgotPasswordAsync(email);
        if (!result) return NotFound(new { message = "Email not found" });
        return Ok(new { message = "Reset link sent to your email" });
    }
    catch (Exception ex) {
        // إرسال تفاصيل الخطأ للفرونت إند للتشخيص
        return StatusCode(500, new { message = "Error sending email", details = ex.Message });
    }
}

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromQuery] string token, [FromBody] string newPassword)
        {
            var result = await _authService.ResetPasswordAsync(token, newPassword);
            if (!result) return BadRequest(new { message = "Invalid or expired token" });

            return Ok(new { message = "Password reset successfully" });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("activate/{userId}")]
        public async Task<IActionResult> ActivateUser(int userId)
        {
            var result = await _authService.ActivateUserAsync(userId);
            if (!result) return NotFound(new { message = "User not found" });

            return Ok(new { message = "User activated successfully" });
        }
    }
}
