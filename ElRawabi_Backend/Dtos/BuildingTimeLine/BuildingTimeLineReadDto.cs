namespace ElRawabi_Backend.Dtos.BuildingTimeLine
{
    public class BuildingTimeLineReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string StageName { get; set; }
        public bool? IsCompleted { get; set; }
        public DateTime CompletionDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string BuildingName { get; set; }
    }
}