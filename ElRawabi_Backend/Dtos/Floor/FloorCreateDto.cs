using System.ComponentModel.DataAnnotations;

namespace ElRawabi_Backend.Dtos.Floor
{
    public class FloorCreateDto
    {
        [Required]
        public int FloorNumber { get; set; }
        [Required]
        public int BuildingId { get; set; }
    }
}
