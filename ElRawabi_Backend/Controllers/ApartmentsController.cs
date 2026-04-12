using ElRawabi_Backend.Dtos.Apartment;
using ElRawabi_Backend.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElRawabi_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentsController : ControllerBase
    {
        private readonly IApartmentService _apartmentService;

        public ApartmentsController(IApartmentService apartmentService)
        {
            _apartmentService = apartmentService;
        }

        [HttpGet("Get-All-Apartments")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var apartment = await _apartmentService.GetAllApartmentsAsync();
            return Ok(apartment);
        }

        [HttpGet("Apartment-Details/{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var apartment = await _apartmentService.GetApartmentByIdAsync(id);
            return apartment != null ? Ok(apartment) : NotFound("Apartment Not Found");
        }

        [HttpGet("Apartments-Count")]
        [Authorize]
        public async Task<IActionResult> GetCount()
        {
            var count = await _apartmentService.GetApartmentsCountAsync();
            return Ok(new { totalApartments = count });
        }

        [HttpPost("Add-New-Apartment")]
        [Authorize]
        public async Task<IActionResult> Create(ApartmentCreateDto dto)
        {
            var result = await _apartmentService.AddApartmentAsync(dto);
            return result == "Success" ? Ok(new { message = result }) : BadRequest(new { message = result });
        }

        [HttpPut("Update-Apartment-Info")]
        [Authorize]
        public async Task<IActionResult> Update(ApartmentUpdateDto dto)
        {
            try { return Ok(await _apartmentService.UpdateApartmentAsync(dto)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("Remove-Apartment/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _apartmentService.DeleteApartmentAsync(id);
            return result ? Ok("Apartment Deleted Successfully") : NotFound("Apartment Not Found");
        }
    }
}
