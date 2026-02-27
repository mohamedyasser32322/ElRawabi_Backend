using ElRawabi_Backend.Dtos.Users;
using ElRawabi_Backend.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElRawabi_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("Get-All-Users")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("User-Profile/{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound("User Not Exict");
            return Ok(user);
        }

        [HttpGet("Users-Count")]
        [Authorize]
        public async Task<IActionResult> GetCount()
        {
            var count = await _userService.GetUsersCountAsync();
            return Ok(new { totalUsers = count });
        }

        [HttpPut("Update-User-Info")]
        [Authorize]
        public async Task<IActionResult> Update(UserUpdateDto updateDto)
        {
            try
            {
                var updatedUser = await _userService.UpdateUserAsync(updateDto);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Remove-User/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.DeleteAsync(id);
            if (!result) return NotFound("User Not Exict or Already Deleted");
            return Ok("User Deleted Successfully");
        }
    }
}
