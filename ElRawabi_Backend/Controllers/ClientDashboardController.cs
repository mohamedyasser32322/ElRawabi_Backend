using ElRawabi_Backend.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ElRawabi_Backend.Controllers
{
    public class ClientDashboardController : ControllerBase
    {
        private readonly IClientDashboardService _dashboardService;

        public ClientDashboardController(IClientDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("header/{apartmentId}")]
        public async Task<IActionResult> GetHeader(int apartmentId)
        {
            var result = await _dashboardService.GetClientDashboardHeaderAsync(apartmentId);

            if (result == null)
            {
                return NotFound(new { message = "Apartment not found" });
            }

            return Ok(result);
        }
    }
}
