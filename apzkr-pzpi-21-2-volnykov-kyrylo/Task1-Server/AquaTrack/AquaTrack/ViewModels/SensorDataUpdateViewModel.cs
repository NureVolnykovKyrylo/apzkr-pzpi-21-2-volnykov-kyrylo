namespace AquaTrack.ViewModels
{
    public class SensorDataUpdateViewModel
    {
        public int SensorDataId { get; set; }
        public double SensorValue { get; set; }
        public DateTime? Timestamp { get; set; }
        public string? SensorStatus { get; set; }
    }
}
