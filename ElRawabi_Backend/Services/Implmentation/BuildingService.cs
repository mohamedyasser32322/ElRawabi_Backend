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
        private readonly IBuildingImgRepository _buildingImgRepository;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public BuildingService(
            IBuildingRepository buildingRepository,
            IBuildingImgRepository buildingImgRepository,
            IImageService imageService,
            IMapper mapper)
        {
            _buildingRepository = buildingRepository;
            _buildingImgRepository = buildingImgRepository;
            _imageService = imageService;
            _mapper = mapper;
        }

        public async Task<List<AllBuildingsDto>> GetAllBuildingsAsync()
        {
            var buildings = await _buildingRepository.GetAllAsync();
            return _mapper.Map<List<AllBuildingsDto>>(buildings);
        }

        public async Task<BuildingReadDto?> GetBuildingByIdAsync(int id)
        {
            var building = await _buildingRepository.GetByIdAsync(id);
            return building != null ? _mapper.Map<BuildingReadDto>(building) : null;
        }

        public async Task<int> GetBuildingsCountAsync()
        {
            return await _buildingRepository.GetCountAsync();
        }

        public async Task<string> AddBuildingAsync(BuildingCreateDto dto, List<IFormFile>? images = null)
        {
            var existing = await _buildingRepository.GetByBuildingNumberAsync(dto.BuildingNumber, dto.ProjectId);
            if (existing != null) return "هذه العمارة موجودة بالفعل في هذا المشروع";

            var building = _mapper.Map<Building>(dto);
            await _buildingRepository.AddAsync(building);

            // رفع الصور إذا وُجدت
            if (images != null && images.Count > 0)
            {
                var urls = await _imageService.UploadImagesAsync(images, "buildings");
                foreach (var url in urls)
                {
                    await _buildingImgRepository.AddAsync(new BuildingImg
                    {
                        ImgUrl = url,
                        BuildingId = building.Id
                    });
                }
            }

            return "Success";
        }

        public async Task<List<AllBuildingsDto>> GetBuildingsByProjectIdAsync(int projectId)
        {
            var buildings = await _buildingRepository.GetByProjectIdAsync(projectId);
            return _mapper.Map<List<AllBuildingsDto>>(buildings);
        }

        public async Task<BuildingReadDto> UpdateBuildingAsync(BuildingUpdateDto dto, List<IFormFile>? newImages = null)
        {
            var building = await _buildingRepository.GetByIdAsync(dto.Id);
            if (building == null) throw new Exception("العمارة غير موجودة");

            _mapper.Map(dto, building);
            building.LastUpdatedAt = DateTime.UtcNow;
            await _buildingRepository.UpdateAsync(building);

            // رفع صور جديدة إذا وُجدت
            if (newImages != null && newImages.Count > 0)
            {
                var urls = await _imageService.UploadImagesAsync(newImages, "buildings");
                foreach (var url in urls)
                {
                    await _buildingImgRepository.AddAsync(new BuildingImg
                    {
                        ImgUrl = url,
                        BuildingId = building.Id
                    });
                }
            }

            return _mapper.Map<BuildingReadDto>(building);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var building = await _buildingRepository.GetByIdAsync(id);
            if (building == null) return false;

            building.IsDeleted = true;
            building.LastUpdatedAt = DateTime.UtcNow;
            await _buildingRepository.UpdateAsync(building);
            return true;
        }

        public async Task<BuildingImg?> GetBuildingImageByIdAsync(int imageId)
        {
            return await _buildingImgRepository.GetByIdAsync(imageId);
        }

        public async Task<bool> DeleteBuildingImageAsync(int imageId)
        {
            var img = await _buildingImgRepository.GetByIdAsync(imageId);
            if (img == null) return false;

            await _imageService.DeleteImageAsync(img.ImgUrl!);
            img.IsDeleted = true;
            img.LastUpdatedAt = DateTime.UtcNow;
            await _buildingImgRepository.UpdateAsync(img);
            return true;
        }
    }
}
