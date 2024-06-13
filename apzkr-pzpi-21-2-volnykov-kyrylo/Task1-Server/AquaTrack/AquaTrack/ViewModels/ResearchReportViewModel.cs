namespace AquaTrack.ViewModels
{
    public class ResearchReportViewModel
    {
        public int ResearchReportId { get; set; }
        public int AquariumId { get; set; }
        public DateTime CreationDate { get; set; }

        public List<SensorDataViewModel> SensorData { get; set; }
    }
}
