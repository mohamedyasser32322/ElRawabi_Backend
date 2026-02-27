using AutoMapper;
using ElRawabi_Backend.Dtos.Apartment;
using ElRawabi_Backend.Dtos.Projects;
using ElRawabi_Backend.Models;
using ElRawabi_Backend.Repository.Implementation;
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

        public async Task<List<AllApartmentsDto>> GetAllApartmentsAsync()
        {
            var apartments = await _apartmentRepository.GetAllAsync();
            return _mapper.Map<List<AllApartmentsDto>>(apartments);
        }

        public async Task<ApartmentReadDto> GetApartmentByIdAsync(int id)
        {
            var apartment = await _apartmentRepository.GetByIdAsync(id);
            if (apartment != null)
            {
                return _mapper.Map<ApartmentReadDto>(apartment);
            }
            else
            {
                return null;
            }
        }

        public async Task<int> GetApartmentsCountAsync()
        {
            var apartmentsCount = await _apartmentRepository.GetCountAsync();
            return apartmentsCount;
        }

        public async Task<string> AddApartmentAsync(ApartmentCreateDto apartmentCreateDto)
        {
            var exictingApartment = await _apartmentRepository.GetByApartmentNumberAsync(apartmentCreateDto.ApartmentNumber ,apartmentCreateDto.BuildingId ?? 0);
            if (exictingApartment != null) return "Apartment Number Already Exists in this Building";

            var apartment = _mapper.Map<Apartment>(apartmentCreateDto);
            await _apartmentRepository.AddAsync(apartment);
            return "Success";
        }

        public async Task<ApartmentReadDto> UpdateApartmentAsync(ApartmentUpdateDto apartmentUpdateDto)
        {
            var apartment = await _apartmentRepository.GetByIdAsync(apartmentUpdateDto.Id);
            if (apartment == null) throw new Exception("Apartment Not Found");

            _mapper.Map(apartmentUpdateDto, apartment);

            if (apartmentUpdateDto.ClientId != null && apartmentUpdateDto.ClientId > 0)
            {
                apartment.IsSold = true;
            }

            apartment.LastUpdatedAt = DateTime.UtcNow;
            await _apartmentRepository.UpdateAsync(apartment);

            return _mapper.Map<ApartmentReadDto>(apartment);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var apartment = await _apartmentRepository.GetByIdAsync(id);
            if (apartment == null) return false;

            apartment.IsDeleted = true;
            apartment.LastUpdatedAt = DateTime.UtcNow;
            await _apartmentRepository.UpdateAsync(apartment);
            return true;
        }
    }
}
