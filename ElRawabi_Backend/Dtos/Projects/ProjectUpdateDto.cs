using System.ComponentModel.DataAnnotations;

namespace ElRawabi_Backend.Dtos.Projects
{
    public class ProjectUpdateDto
    {
        public int Id { get; set; }
        [StringLength(200)]
        public string? Name { get; set; }
        [StringLength(1000)]
        public string? Description { get; set; }
        [StringLength(1000)]
        public string? Location { get; set; }
    }
}
