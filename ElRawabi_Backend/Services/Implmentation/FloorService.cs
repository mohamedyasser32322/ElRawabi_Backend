using AutoMapper;
using ElRawabi_Backend.Dtos.Floor;
using ElRawabi_Backend.Models;
using ElRawabi_Backend.Repository.Interfaces;
using ElRawabi_Backend.Services.Interface;

namespace ElRawabi_Backend.Services.Implmentation
{
    public class FloorService : IFloorService
    {
        private readonly IFloorRepository _floorRepository;
        private readonly IMapper _mapper;

        public FloorService(IFloorRepository floorRepository, IMapper mapper)
        {
            _floorRepository = floorRepository;
            _mapper = mapper;
        }

        public async Task<List<FloorReadDto>> GetFloorsByBuildingIdAsync(int buildingId)
        {
            var floors = await _floorRepository.GetFloorsByBuildingIdAsync(buildingId);
            return _mapper.Map<List<FloorReadDto>>(floors);
        }

        public async Task<FloorReadDto?> GetFloorByIdAsync(int id)
        {
            var floor = await _floorRepository.GetByIdAsync(id);
            return floor != null ? _mapper.Map<FloorReadDto>(floor) : null;
        }

        public async Task<string> AddFloorAsync(FloorCreateDto dto)
        {
            var existing = await _floorRepository.GetByFloorNumberAndBuildingIdAsync(dto.FloorNumber, dto.BuildingId);
            if (existing != null) return "هذا الدور موجود بالفعل في هذه العمارة";

            var floor = _mapper.Map<Floor>(dto);
            await _floorRepository.AddAsync(floor);
            return "Success";
        }

        public async Task<FloorReadDto> UpdateFloorAsync(FloorUpdateDto dto)
        {
            var floor = await _floorRepository.GetByIdAsync(dto.Id);
            if (floor == null) throw new Exception("الدور غير موجود");

            _mapper.Map(dto, floor);
            floor.LastUpdatedAt = DateTime.UtcNow;
            await _floorRepository.UpdateAsync(floor);
            return _mapper.Map<FloorReadDto>(floor);
        }

        public async Task<bool> DeleteFloorAsync(int id)
        {
            var floor = await _floorRepository.GetByIdAsync(id);
            if (floor == null) return false;

            floor.IsDeleted = true;
            floor.LastUpdatedAt = DateTime.UtcNow;
            await _floorRepository.UpdateAsync(floor);
            return true;
        }
    }
}
