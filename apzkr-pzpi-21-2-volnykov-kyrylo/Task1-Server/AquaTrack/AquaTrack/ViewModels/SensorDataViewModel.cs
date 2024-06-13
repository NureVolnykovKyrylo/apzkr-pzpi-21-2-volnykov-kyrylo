using AquaTrack.Models;

namespace AquaTrack.ViewModels
{
    public class SensorDataViewModel
    {
        public int SensorDataId { get; set; }
        public SensorType SensorType { get; set; }
        public double SensorValue { get; set; }
        public DateTime? Timestamp { get; set; }
        public string? SensorStatus { get; set; }
        public string SensorIdentificator { get; set; }
    }
}
