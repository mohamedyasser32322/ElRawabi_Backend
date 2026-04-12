using ElRawabi_Backend.Dtos.Floor;
using System.ComponentModel.DataAnnotations;

namespace ElRawabi_Backend.Dtos.Buildings
{
    public class BuildingReadDto
    {
        public int Id { get; set; }
        public string? BuildingNumber { get; set; }
        public int TotalFloors { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public List<FloorReadDto> Floors { get; set; } = new List<FloorReadDto>();
        public List<string> ImageUrls { get; set; } = new List<string>();
    }
}
