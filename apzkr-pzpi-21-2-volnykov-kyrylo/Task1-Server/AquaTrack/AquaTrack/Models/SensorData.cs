namespace AquaTrack.Models
{
    public enum SensorType
    {
        TemperatureSensor,
        WaterLevelSensor,
        LightingSensor,
        AciditySensor,
        MovementSensor,
        VideoSensor
    }

    public class SensorData
    {
        public int SensorDataId { get; set; }
        public SensorType SensorType { get; set; }
        public double SensorValue { get; set; }
        public DateTime? Timestamp { get; set; }
        public string? SensorStatus { get; set; }
        public string SensorIdentificator { get; set; }

        public List<ResearchReport> ResearchReports { get; set; }
    }
}
