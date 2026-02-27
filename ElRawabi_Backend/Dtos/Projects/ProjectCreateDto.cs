using System.ComponentModel.DataAnnotations;

namespace ElRawabi_Backend.Dtos.Projects
{
    public class ProjectCreateDto
    {
        [Required, StringLength(200)]
        public string? Name { get; set; }
        [Required, StringLength(1000)]
        public string? Description { get; set; }
        [Required, StringLength(1000)]
        public string? Location { get; set; }
    }
}
