using ElRawabi_Backend.Dtos.Buildings;
using ElRawabi_Backend.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElRawabi_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BuildingsController : ControllerBase
    {
        private readonly IBuildingService _buildingService;

        public BuildingsController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        [HttpGet("Get-All-Buildings")]
        public async Task<IActionResult> GetAll()
            => Ok(await _buildingService.GetAllBuildingsAsync());

        [HttpGet("Building-Profile/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var building = await _buildingService.GetBuildingByIdAsync(id);
            return building != null ? Ok(building) : NotFound("العمارة غير موجودة");
        }

        [HttpGet("Get-Project-Buildings/{projectId}")]
        public async Task<IActionResult> GetByProject(int projectId)
            => Ok(await _buildingService.GetBuildingsByProjectIdAsync(projectId));

        [HttpGet("Buildings-Count")]
        public async Task<IActionResult> GetCount()
            => Ok(new { totalBuildings = await _buildingService.GetBuildingsCountAsync() });

        // ✅ تم التعديل: يقبل [FromForm] لدعم رفع الصور
        [HttpPost("Create-Building")]
        public async Task<IActionResult> Create([FromForm] BuildingCreateDto dto, [FromForm] List<IFormFile>? images)
        {
            var result = await _buildingService.AddBuildingAsync(dto, images);
            return result == "Success" ? Ok(new { message = result }) : BadRequest(new { message = result });
        }

        // ✅ تم التعديل: يقبل [FromForm] لدعم رفع الصور
        [HttpPut("Update-Building-Info")]
        public async Task<IActionResult> Update([FromForm] BuildingUpdateDto dto, [FromForm] List<IFormFile>? newImages)
        {
            try { return Ok(await _buildingService.UpdateBuildingAsync(dto, newImages)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("Remove-Building/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _buildingService.DeleteAsync(id);
            return result ? Ok("تم حذف العمارة بنجاح") : NotFound("العمارة غير موجودة");
        }

        // ✅ جديد: حذف صورة معينة من العمارة
        [HttpDelete("Delete-Building-Image/{imageId}")]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            var img = await _buildingService.GetBuildingImageByIdAsync(imageId);
            if (img == null) return NotFound("الصورة غير موجودة");

            var result = await _buildingService.DeleteBuildingImageAsync(imageId);
            return result ? Ok("تم حذف الصورة بنجاح") : BadRequest("فشل حذف الصورة");
        }
    }
}
