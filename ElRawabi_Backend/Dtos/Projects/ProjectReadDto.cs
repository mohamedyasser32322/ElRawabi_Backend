using System.ComponentModel.DataAnnotations;

namespace ElRawabi_Backend.Dtos.Projects
{
    public class ProjectReadDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
    }
}
