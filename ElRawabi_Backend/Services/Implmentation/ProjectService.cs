using AutoMapper;
using ElRawabi_Backend.Dtos.Projects;
using ElRawabi_Backend.Dtos.Users;
using ElRawabi_Backend.Models;
using ElRawabi_Backend.Repository.Implementation;
using ElRawabi_Backend.Repository.Interfaces;
using ElRawabi_Backend.Services.Interface;

namespace ElRawabi_Backend.Services.Implmentation
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        public ProjectService(IProjectRepository projectRepository,IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<List<AllProjectsDto>> GetAllProjectsAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return _mapper.Map<List<AllProjectsDto>>(projects);
        }

        public async Task<ProjectReadDto> GetProjectByIdAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project != null)
            {
                return _mapper.Map<ProjectReadDto>(project);
            }
            else
            {
                return null;
            }
        }

        public async Task<int> GetProjectsCountAsync()
        {
            var projectsCount = await _projectRepository.GetCountAsync();
            return projectsCount;
        }

        public async Task<string> AddProjectAsync(ProjectCreateDto projectCreateDto)
        {
            var exictingProject = await _projectRepository.GetByProjectNameAsync(projectCreateDto.Name);
            if (exictingProject != null) return "Project Already Exists";

            var project = _mapper.Map<Project>(projectCreateDto);
            await _projectRepository.AddAsync(project);
            return "Success";
        }

        public async Task<ProjectReadDto> UpdateProjectAsync(ProjectUpdateDto projectUpdateDto)
        {
            var project = await _projectRepository.GetByIdAsync(projectUpdateDto.Id);
            if (project == null)
            {
                throw new Exception("Project Not Exists");
            }
            else
            {
                _mapper.Map(projectUpdateDto, project);
                project.LastUpdatedAt = DateTime.UtcNow;
                await _projectRepository.UpdateAsync(project);
                return _mapper.Map<ProjectReadDto>(project);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project != null)
            {
                project.IsDeleted = true;
                project.LastUpdatedAt = DateTime.UtcNow;
                await _projectRepository.UpdateAsync(project);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
