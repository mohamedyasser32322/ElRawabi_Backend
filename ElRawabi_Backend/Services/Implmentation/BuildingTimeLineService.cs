using AutoMapper;
using ElRawabi_Backend.Dtos.BuildingTimeLine;
using ElRawabi_Backend.Models;
using ElRawabi_Backend.Repository.Implementation;
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

        public async Task<BuildingTimeLineReadDto?> GetBuildingTimeLineByIdAsync(int id)
        {
            var stage = await _buildingTimeLineRepo.GetByIdAsync(id);
            if (stage == null) return null;

            return _mapper.Map<BuildingTimeLineReadDto>(stage);
        }

        public async Task<string> AddBuildingTimeLineAsync(BuildingTimeLineCreateDto dto)
        {
            var existingStages = await _buildingTimeLineRepo.GetAllAsync();
            var isDuplicate = existingStages.Any(s => s.BuildingId == dto.BuildingId && s.Stage == dto.Stage && !s.IsDeleted);

            if (isDuplicate) return "هذه المرحلة مضافة بالفعل لهذا المبنى";

            var entity = _mapper.Map<BuildingTimeLine>(dto);

            if (entity.IsCompleted == true)
            {
                entity.CompletedAt = DateTime.UtcNow;
            }
            else
            {
                entity.CompletedAt = null;
            }

            await _buildingTimeLineRepo.AddAsync(entity);
            return "Success";
        }

        public async Task<BuildingTimeLineReadDto?> UpdateBuildingTimeLineAsync(BuildingTimeLineUpdateDto dto)
        {
            var existingStage = await _buildingTimeLineRepo.GetByIdAsync(dto.Id);
            if (existingStage == null) return null;

            bool wasCompleted = existingStage.IsCompleted ?? false;

            _mapper.Map(dto, existingStage);
            existingStage.UpdatedAt = DateTime.UtcNow;

            if (existingStage.IsCompleted == true && !wasCompleted)
            {
                existingStage.CompletedAt = dto.CompletedAt ?? DateTime.UtcNow;
            }
            else if (existingStage.IsCompleted == false)
            {
                existingStage.CompletedAt = null;
            }

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

        public async Task<List<BuildingTimeLineReadDto>> GetBuildingTimeLinesByBuildingIdAsync(int buildingId)
        {
            var timeLines = await _buildingTimeLineRepo.GetBuildingTimeLinesByBuildingIdAsync(buildingId);
            return _mapper.Map<List<BuildingTimeLineReadDto>>(timeLines);
        }

    }
}