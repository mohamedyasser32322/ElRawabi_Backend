using AutoMapper;
using ElRawabi_Backend.Dtos.BuildingTimeLine;
using ElRawabi_Backend.Models;
using ElRawabi_Backend.Repository.Interfaces;
using ElRawabi_Backend.Services.Interface;

namespace ElRawabi_Backend.Services.Implmentation
{
    public class BuildingTimeLineService : IBuildingTimeLineService
    {
        private readonly IBuildingTimeLineRepository _buildingTimeLineRepo;
        private readonly IMapper _mapper;
        public BuildingTimeLineService(IBuildingTimeLineRepository buildingTimeLineRepo, IMapper mapper)
        {
            _buildingTimeLineRepo = buildingTimeLineRepo;
            _mapper = mapper;
        }

        public async Task<BuildingTimeLineReadDto> GetBuildingTimeLineByIdAsync(int id)
        {
            var stage = await _buildingTimeLineRepo.GetByIdAsync(id);
            if (stage == null) return null;
            return _mapper.Map<BuildingTimeLineReadDto>(stage);
        }

        public async Task<string> AddBuildingTimeLineAsync(BuildingTimeLineCreateDto dto)
        {
            var entity = _mapper.Map<BuildingTimeLine>(dto);
            var result = await _buildingTimeLineRepo.AddAsync(entity);
            return "Success";
        }

        public async Task<BuildingTimeLineReadDto> UpdateBuildingTimeLineAsync(BuildingTimeLineUpdateDto dto)
        {
            var existingStage = await _buildingTimeLineRepo.GetByIdAsync(dto.Id);
            if (existingStage == null) return null;

            _mapper.Map(dto, existingStage);
            existingStage.UpdatedAt = DateTime.UtcNow;

            var updated = await _buildingTimeLineRepo.UpdateAsync(existingStage);
            return _mapper.Map<BuildingTimeLineReadDto>(updated);
        }

        public async Task<bool> DeleteBuildingTimeLineAsync(int id)
        {
            var stage = await _buildingTimeLineRepo.GetByIdAsync(id);
            if (stage == null) return false;

            stage.IsDeleted = true;
            await _buildingTimeLineRepo.UpdateAsync(stage);
            return true;
        }
    }
}
