namespace AquaTrack.Models
{
    public class ResearchReport
    {
        public int ResearchReportId { get; set; }
        public int AnalysisReportId { get; set; }
        public int AquariumId { get; set; }
        public DateTime CreationDate { get; set; }

        public AnalysisReport AnalysisReport { get; set; }
        public Aquarium Aquarium { get; set; }
        public List<SensorData> SensorData { get; set; }
    }
}
