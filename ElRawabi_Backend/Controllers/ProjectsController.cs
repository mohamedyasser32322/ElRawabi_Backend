using ElRawabi_Backend.Dtos.Projects;
using ElRawabi_Backend.Dtos.Users;
using ElRawabi_Backend.Services.Implmentation;
using ElRawabi_Backend.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElRawabi_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("Get-All-Projects")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            return Ok(projects);
        }

        [HttpGet("Project-Profile/{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null) return NotFound("Project Not Exists");
            return Ok(project);
        }

        [HttpGet("Projects-Count")]
        [Authorize]
        public async Task<IActionResult> GetCount()
        {
            var count = await _projectService.GetProjectsCountAsync();
            return Ok(new { totalProjects = count });
        }

        [HttpPost("Create-Project")]
        public async Task<IActionResult> Create( ProjectCreateDto projectCreateDto)
        {
            var result = await _projectService.AddProjectAsync(projectCreateDto);

            if (result == "Project Already Exists")
                return BadRequest(new { message = result });

            return Ok(new { message = "Project Created successfully" });
        }

        [HttpPut("Update-Project-Info")]
        [Authorize]
        public async Task<IActionResult> Update(ProjectUpdateDto updateDto)
        {
            try
            {
                var updatedProject = await _projectService.UpdateProjectAsync(updateDto);
                return Ok(updatedProject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Remove-Project/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _projectService.DeleteAsync(id);
            if (!result) return NotFound("Project Not Exist or Already Deleted");
            return Ok("Project Deleted Successfully");
        }
    }
}
