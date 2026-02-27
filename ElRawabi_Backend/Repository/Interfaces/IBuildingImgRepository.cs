using ElRawabi_Backend.Models;

namespace ElRawabi_Backend.Repository.Interfaces
{
    public interface IBuildingImgRepository
    {
        Task<List<BuildingImg>> GetAllAsync();
        Task<BuildingImg?> GetByIdAsync(int id);
        Task<BuildingImg> AddAsync(BuildingImg BuildingImg);
        Task<BuildingImg> UpdateAsync(BuildingImg BuildingImg);
    }
}
