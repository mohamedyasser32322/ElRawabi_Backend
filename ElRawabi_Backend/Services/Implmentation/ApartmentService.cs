using AutoMapper;
using ElRawabi_Backend.Dtos.Apartment;
using ElRawabi_Backend.Models;
using ElRawabi_Backend.Repository.Interfaces;
using ElRawabi_Backend.Services.Interface;

namespace ElRawabi_Backend.Services.Implmentation
{
    public class ApartmentService : IApartmentService
    {
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IMapper _mapper;

        public ApartmentService(IApartmentRepository apartmentRepository, IMapper mapper)
        {
            _apartmentRepository = apartmentRepository;
            _mapper = mapper;
        }

        public async Task<List<ApartmentReadDto>> GetAllApartmentsAsync()
        {
            var apartments = await _apartmentRepository.GetAllAsync();
            return _mapper.Map<List<ApartmentReadDto>>(apartments);
        }

        public async Task<ApartmentReadDto?> GetApartmentByIdAsync(int id)
        {
            var apartment = await _apartmentRepository.GetByIdAsync(id);
            return apartment != null ? _mapper.Map<ApartmentReadDto>(apartment) : null;
        }

        public async Task<List<ApartmentReadDto>> GetApartmentsByFloorIdAsync(int floorId)
        {
            var apartments = await _apartmentRepository.GetApartmentsByFloorIdAsync(floorId);
            return _mapper.Map<List<ApartmentReadDto>>(apartments);
        }

        public async Task<string> AddApartmentAsync(ApartmentCreateDto dto)
        {
            var existing = await _apartmentRepository.GetByApartmentNumberAndFloorIdAsync(dto.ApartmentNumber, dto.FloorId);
            if (existing != null) return "هذه الشقة موجودة بالفعل في هذا الدور";

            var apartment = _mapper.Map<Apartment>(dto);

            // ✅ لوجيك تلقائي للحالة
            if (dto.ClientId.HasValue && dto.ClientId > 0)
            {
                apartment.Status = ApartmentStatus.Sold;
            }
            else
            {
                apartment.Status = dto.Status;
            }

            await _apartmentRepository.AddAsync(apartment);
            return "Success";
        }

        public async Task<ApartmentReadDto> UpdateApartmentAsync(ApartmentUpdateDto dto)
        {
            var apartment = await _apartmentRepository.GetByIdAsync(dto.Id);
            if (apartment == null) throw new Exception("الشقة غير موجودة");

            _mapper.Map(dto, apartment);

            if (dto.ClientId.HasValue && dto.ClientId > 0)
            {
                apartment.Status = ApartmentStatus.Sold;
            }
            else
            {
                apartment.Status = dto.Status;
            }

            apartment.LastUpdatedAt = DateTime.UtcNow;
            await _apartmentRepository.UpdateAsync(apartment);
            return _mapper.Map<ApartmentReadDto>(apartment);
        }

        public async Task<bool> DeleteApartmentAsync(int id)
        {
            var apartment = await _apartmentRepository.GetByIdAsync(id);
            if (apartment == null) return false;

            apartment.IsDeleted = true;
            apartment.LastUpdatedAt = DateTime.UtcNow;
            await _apartmentRepository.UpdateAsync(apartment);
            return true;
        }

        public async Task<int> GetApartmentsCountAsync()
        {
            return await _apartmentRepository.GetCountAsync();
        }
    }
}
