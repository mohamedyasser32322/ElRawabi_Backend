using ElRawabi_Backend.Dtos.Projects;

namespace ElRawabi_Backend.Services.Interface
{
    public interface IProjectService
    {
        Task<List<AllProjectsDto>> GetAllProjectsAsync();
        Task<ProjectReadDto> GetProjectByIdAsync(int  id);
        Task<int> GetProjectsCountAsync();
        Task<string> AddProjectAsync(ProjectCreateDto projectCreateDto);
        Task<ProjectReadDto> UpdateProjectAsync(ProjectUpdateDto projectUpdateDto);
        Task<bool> DeleteAsync(int id);
    }
}