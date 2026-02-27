using AutoMapper;
using ElRawabi_Backend.Dtos.Buildings;
using ElRawabi_Backend.Dtos.Projects;
using ElRawabi_Backend.Models;
using ElRawabi_Backend.Repository.Implementation;
using ElRawabi_Backend.Repository.Interfaces;
using ElRawabi_Backend.Services.Interface;

namespace ElRawabi_Backend.Services.Implmentation
{
    public class BuildingService : IBuildingService
    {
        private readonly IBuildingRepository _buildingRepository;
        private readonly IMapper _mapper;
        public BuildingService(IBuildingRepository buildingRepository, IMapper mapper)
        {
            _buildingRepository = buildingRepository;
            _mapper = mapper;
        }

        public async Task<List<AllBuildingsDto>> GetAllBuildingsAsync()
        {
            var buildings = await _buildingRepository.GetAllAsync();
            return _mapper.Map<List<AllBuildingsDto>>(buildings);
        }

        public async Task<BuildingReadDto> GetBuildingByIdAsync(int id)
        {
            var building = await _buildingRepository.GetByIdAsync(id);
            if (building != null)
            {
                return _mapper.Map<BuildingReadDto>(building);
            }
            else
            {
                return null;
            }
        }

        public async Task<int> GetBuildingsCountAsync()
        {
            var buildingsCount = await _buildingRepository.GetCountAsync();
            return buildingsCount;
        }

        public async Task<string> AddBuildingAsync(BuildingCreateDto buildingCreateDto)
        {
            var exictingBuilding = await _buildingRepository.GetByBuildingNumberAsync(
            buildingCreateDto.BuildingNumber,
            buildingCreateDto.ProjectId
            );

            if (exictingBuilding != null) return "Building Already Exists in this Project";

            var building = _mapper.Map<Building>(buildingCreateDto);
            await _buildingRepository.AddAsync(building);
            return "Success";
        }

        public async Task<List<AllBuildingsDto>> GetBuildingsByProjectIdAsync(int projectId)
        {
            var buildings = await _buildingRepository.GetByProjectIdAsync(projectId);
            return _mapper.Map<List<AllBuildingsDto>>(buildings);
        }


        public async Task<BuildingReadDto> UpdateBuildingAsync(BuildingUpdateDto buildingUpdateDto)
        {
            var building = await _buildingRepository.GetByIdAsync(buildingUpdateDto.Id);
            if (building == null)
            {
                throw new Exception("Building Not Exists");
            }
            else
            {
                _mapper.Map(buildingUpdateDto, building);
                building.LastUpdatedAt = DateTime.UtcNow;
                await _buildingRepository.UpdateAsync(building);
                return _mapper.Map<BuildingReadDto>(building);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var building = await _buildingRepository.GetByIdAsync(id);
            if (building != null)
            {
                building.IsDeleted = true;
                building.LastUpdatedAt = DateTime.UtcNow;
                await _buildingRepository.UpdateAsync(building);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
