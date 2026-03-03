using ElRawabi_Backend.Dtos.BuildingTimeLine;
using ElRawabi_Backend.Services.Implmentation;
using ElRawabi_Backend.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElRawabi_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BuildingTimeLineController : ControllerBase
    {
        private readonly IBuildingTimeLineService _buildingTimeLineService;
        public BuildingTimeLineController(IBuildingTimeLineService buildingTimeLineService)
        {
            _buildingTimeLineService = buildingTimeLineService;
        }

        [HttpGet("TimeLine-Profile/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _buildingTimeLineService.GetBuildingTimeLineByIdAsync(id);
            if (result == null) return NotFound("Stage Not Exists");
            return Ok(result);
        }

        [HttpPost("Create-TimeLine")]
        public async Task<IActionResult> Create(BuildingTimeLineCreateDto dto)
        {
            var result = await _buildingTimeLineService.AddBuildingTimeLineAsync(dto);
            return Ok(new { message = result });
        }

        [HttpPut("Update-TimeLine-Info")]
        public async Task<IActionResult> Update(BuildingTimeLineUpdateDto dto)
        {
            try
            {
                var updatedTimeLine = await _buildingTimeLineService.UpdateBuildingTimeLineAsync(dto);
                return Ok(updatedTimeLine);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Remove-TimeLine/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _buildingTimeLineService.DeleteBuildingTimeLineAsync(id);
            if (!success) return NotFound("Stage Not Exist or Already Deleted");
            return Ok(new { message = "Stage Deleted Successfully" });
        }
    }
}
