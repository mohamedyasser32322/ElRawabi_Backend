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
            var response = await _authService.LoginAsync(loginDto);
            if (response == null)
                return Unauthorized(new { message = "Invalid email or password" });

            return Ok(response);
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

            return Ok(new { message = "User registered successfully" });
        }
    }
}
