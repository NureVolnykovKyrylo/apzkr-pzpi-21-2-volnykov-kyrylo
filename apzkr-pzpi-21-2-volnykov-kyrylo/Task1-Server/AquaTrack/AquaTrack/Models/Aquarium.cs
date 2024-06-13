namespace AquaTrack.Models
{
    public class Aquarium
    {
        public int AquariumId { get; set; }
        public int UserId { get; set; }
        public string AquariumType { get; set; }
        public float? Acidity { get; set; }
        public float? WaterLevel { get; set; }
        public float? Temperature { get; set; }
        public float? Lighting { get; set; }

        public User User { get; set; }
        public List<Inhabitant> Inhabitants { get; set; }
        public List<FeedingSchedule> FeedingSchedules { get; set; }
    }
}
