using ElRawabi_Backend.Dtos.Buildings;
using ElRawabi_Backend.Dtos.Projects;
using ElRawabi_Backend.Services.Implmentation;
using ElRawabi_Backend.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElRawabi_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingsController : ControllerBase
    {
        private readonly IBuildingService _buildingService;
        public BuildingsController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        [HttpGet("Get-All-Buildings")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var buildings = await _buildingService.GetAllBuildingsAsync();
            return Ok(buildings);
        }

        [HttpGet("Building-Profile/{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var building = await _buildingService.GetBuildingByIdAsync(id);
            if (building == null) return NotFound("Building Not Exists");
            return Ok(building);
        }

        [HttpGet("Buildings-Count")]
        [Authorize]
        public async Task<IActionResult> GetCount()
        {
            var count = await _buildingService.GetBuildingsCountAsync();
            return Ok(new { totalBuildings = count });
        }

        [HttpGet("Get-Project-Buildings/{projectId}")]
        [Authorize]
        public async Task<IActionResult> GetByProject(int projectId)
        {
            var buildings = await _buildingService.GetBuildingsByProjectIdAsync(projectId);
            return Ok(buildings);
        }

        [HttpPost("Create-Building")]
        public async Task<IActionResult> Create(BuildingCreateDto buildingCreateDto)
        {
            var result = await _buildingService.AddBuildingAsync(buildingCreateDto);

            if (result == "Building Already Exists")
                return BadRequest(new { message = result });

            return Ok(new { message = "Building Created successfully" });
        }

        [HttpPut("Update-Building-Info")]
        [Authorize]
        public async Task<IActionResult> Update(BuildingUpdateDto buildingUpdateDto)
        {
            try
            {
                var updatedBuilding = await _buildingService.UpdateBuildingAsync(buildingUpdateDto);
                return Ok(updatedBuilding);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Remove-Building/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _buildingService.DeleteAsync(id);
            if (!result) return NotFound("Building Not Exist or Already Deleted");
            return Ok("Building Deleted Successfully");
        }
    }
}