using ElRawabi_Backend.Dtos.Floor;
using ElRawabi_Backend.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElRawabi_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FloorsController : ControllerBase
    {
        private readonly IFloorService _floorService;

        public FloorsController(IFloorService floorService)
        {
            _floorService = floorService;
        }

        [HttpGet("Get-Building-Floors/{buildingId}")]
        public async Task<IActionResult> GetByBuilding(int buildingId)
        {
            var floors = await _floorService.GetFloorsByBuildingIdAsync(buildingId);
            return Ok(floors);
        }

        [HttpGet("Floor-Details/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var floor = await _floorService.GetFloorByIdAsync(id);
            return floor != null ? Ok(floor) : NotFound("الدور غير موجود");
        }

        [HttpPost("Add-New-Floor")]
        public async Task<IActionResult> Create([FromBody] FloorCreateDto dto)
        {
            var result = await _floorService.AddFloorAsync(dto);
            return result == "Success" ? Ok(new { message = result }) : BadRequest(new { message = result });
        }

        [HttpPut("Update-Floor-Info")]
        public async Task<IActionResult> Update([FromBody] FloorUpdateDto dto)
        {
            try { return Ok(await _floorService.UpdateFloorAsync(dto)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("Remove-Floor/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _floorService.DeleteFloorAsync(id);
            return result ? Ok("تم حذف الدور بنجاح") : NotFound("الدور غير موجود");
        }
    }
}
