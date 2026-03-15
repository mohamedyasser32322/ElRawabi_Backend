using ElRawabi_Backend.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ElRawabi_Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientDashboardController : ControllerBase
    {
        private readonly IClientDashboardService _dashboardService;

        public ClientDashboardController(IClientDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("header")]
        public async Task<IActionResult> GetHeader()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized(new { message = "بيانات المستخدم غير موجودة" });
            }

            var result = await _dashboardService.GetClientDashboardHeaderByEmailAsync(userEmail);

            if (result == null)
            {
                return NotFound(new { message = "لم يتم العثور على شقة مربوطة بهذا الحساب" });
            }

            return Ok(result);
        }
    }
}