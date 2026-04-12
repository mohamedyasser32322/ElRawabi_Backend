using System.ComponentModel.DataAnnotations;

namespace ElRawabi_Backend.Dtos.Floor
{
    public class FloorUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int FloorNumber { get; set; }
    }
}
