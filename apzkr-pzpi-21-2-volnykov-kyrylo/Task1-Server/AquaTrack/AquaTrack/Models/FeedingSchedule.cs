namespace AquaTrack.Models
{
    public class FeedingSchedule
    {
        public int FeedingScheduleId { get; set; }
        public int AquariumId { get; set; }
        public float FeedTime { get; set; }
        public float? FeedAmount { get; set; }
        public string? FeedType { get; set; }
        public float? RepeatInterval { get; set; }
        public bool Active { get; set; }

        public Aquarium Aquarium { get; set; }
    }
}
