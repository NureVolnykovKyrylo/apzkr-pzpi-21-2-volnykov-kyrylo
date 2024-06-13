using AquaTrack.Models;

namespace AquaTrack.ViewModels
{
    public class AquariumViewModel
    {
        public int AquariumId { get; set; }
        public string AquariumType { get; set; }
        public float? Acidity { get; set; }
        public float? WaterLevel { get; set; }
        public float? Temperature { get; set; }
        public float? Lighting { get; set; }

    }
}
