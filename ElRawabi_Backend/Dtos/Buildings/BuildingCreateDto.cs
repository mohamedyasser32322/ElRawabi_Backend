using System.ComponentModel.DataAnnotations;

namespace ElRawabi_Backend.Dtos.Buildings
{
    public class BuildingCreateDto
    {
        [Required]
        public string? BuildingNumber { get; set; }
        [Required]
        public int TotalFloors { get; set; }
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public DateTime DeliveryDate { get; set; }
    }
}
