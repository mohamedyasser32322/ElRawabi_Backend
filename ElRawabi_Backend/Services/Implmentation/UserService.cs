using AutoMapper;
using ElRawabi_Backend.Dtos.Users;
using ElRawabi_Backend.Repository.Interfaces;
using ElRawabi_Backend.Services.Interface;

namespace ElRawabi_Backend.Services.Implmentation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository , IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<AllUsersDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<List<AllUsersDto>>(users);
        }

        public async Task<int> GetUsersCountAsync()
        {
            var usersCount = await _userRepository.GetCountAsync();
            return usersCount;
        }

        public async Task<UserReadDto> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                return _mapper.Map<UserReadDto>(user);
            }
            else
            {
                return null;
            }
        }

        public async Task<UserReadDto> UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
            var user = await _userRepository.GetByIdAsync(userUpdateDto.Id);
            if (user == null)
            {
                throw new Exception("User Not Exict");
            }
            else
            {
                _mapper.Map(userUpdateDto, user);
                user.LastUpdatedAt = DateTime.Now;
                await _userRepository.UpdateAsync(user);
                return _mapper.Map<UserReadDto>(user);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                user.IsDeleted = true;
                user.LastUpdatedAt = DateTime.UtcNow;
                await _userRepository.UpdateAsync(user);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
