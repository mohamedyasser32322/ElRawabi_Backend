using System.ComponentModel.DataAnnotations;

namespace ElRawabi_Backend.Models
{
    public class BuildingImg
    {
        public int Id { get; set; }
        [Required,Url]
        public string? ImgUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;

        public Building Building { get; set; }
        public int BuildingId { get; set; }
    }
}
