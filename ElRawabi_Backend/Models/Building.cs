using System.ComponentModel.DataAnnotations;

namespace ElRawabi_Backend.Models
{
    public class Building
    {
        public int Id { get; set; }
        [Required]
        public string? BuildingNumber { get; set; }
        [Required]
        public int TotalFloors {  get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime DeliveryDate { get; set; }
        public Project Project { get; set; }
        public int ProjectId { get; set; }
        public ICollection<Floor> Floors { get; set; } = new List<Floor>();
        public ICollection<BuildingTimeLine> buildingTimeLines { get; set; } = new List<BuildingTimeLine>();
        public ICollection<BuildingImg> BuildingImgs { get; set; } = new List<BuildingImg>();
    }
}
