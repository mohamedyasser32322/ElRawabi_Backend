using System.ComponentModel.DataAnnotations;

namespace ElRawabi_Backend.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Required,StringLength(200)]
        public string? Name { get; set; }
        [Required,StringLength(1000)]
        public string? Description { get; set; }
        [Required, StringLength(1000)]
        public string? Location { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastUpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;

        public ICollection<Building> Buildings { get; set; } = new List<Building>(); 
    }
}
