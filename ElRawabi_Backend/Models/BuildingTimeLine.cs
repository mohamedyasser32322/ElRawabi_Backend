using System.ComponentModel.DataAnnotations;

namespace ElRawabi_Backend.Models
{
    public enum BuildingStage
    {
        Stage1 = 1,
        Stage2 = 2,
        Stage3 = 3,
        Stage4 = 4,
        Stage5 = 5,
        Stage6 = 6,
        Stage7 = 7,
    };
    public class BuildingTimeLine
    {
        public int Id {  get; set; }
        [Required,StringLength(200)]
        public string? Title { get; set; }
        [Required, StringLength(1000)]
        public string? Description { get; set; }
        [Required]
        public BuildingStage Stage { get; set; } = BuildingStage.Stage1;
        public bool? IsCompleted { get; set; }
        public DateTime CompletedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;

        public Building Building { get; set; }
        public int BuildingId { get; set; }
    }
}
