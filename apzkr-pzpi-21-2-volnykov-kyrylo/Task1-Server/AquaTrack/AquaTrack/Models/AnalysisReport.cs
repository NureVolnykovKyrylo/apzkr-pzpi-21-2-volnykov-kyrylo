namespace AquaTrack.Models
{
    public class AnalysisReport
    {
        public int AnalysisReportId { get; set; }
        public int ResearchReportId { get; set; }
        public DateTime CreationDate { get; set; }
        public string Title { get; set; }
        public string IdentifiedTrends { get; set; }
        public string Recommendations { get; set; }

        public ResearchReport ResearchReport { get; set; }
    }
}
