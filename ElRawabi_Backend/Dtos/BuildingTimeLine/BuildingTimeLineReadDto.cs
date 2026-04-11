namespace ElRawabi_Backend.Dtos.BuildingTimeLine
{
    public class BuildingTimeLineReadDto
    {
        public int Id { get; set; }
        public int Stage { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int BuildingId { get; set; }
        public string StageDisplayName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string DateText { get; set; } = string.Empty;
        public int StageNumber { get; set; }
    }
}