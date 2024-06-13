namespace AquaTrack.Models
{
    public class Inhabitant
    {
        public int InhabitantId { get; set; }
        public int AquariumId { get; set; }
        public string Species { get; set; }
        public string Name { get; set; }
        public string? UserNotes { get; set; }
        public string? Behavior { get; set; }
        public string? Condition { get; set; }

        public Aquarium Aquarium { get; set; }
    }
}
