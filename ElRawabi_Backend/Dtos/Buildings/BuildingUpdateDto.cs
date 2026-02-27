using System.ComponentModel.DataAnnotations;

namespace ElRawabi_Backend.Dtos.Buildings
{
    public class BuildingUpdateDto
    {
        public int Id { get; set; }
        public string? BuildingNumber { get; set; }
        public int TotalFloors { get; set; }
        public int ProjectId { get; set; }
    }
}
