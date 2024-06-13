using AquaTrack.ViewModels;

namespace AquaTrack.Services.Interfaces
{
    public interface IResearchReportService
    {
        Task<List<ResearchReportViewModel>> GetAllResearchReports();
        Task<ResearchReportViewModel> GetResearchReportById(int researchReportId);
        Task<ResearchReportViewModel> AddResearchReport(ResearchReportViewModel reportViewModel);
        Task<ResearchReportViewModel> UpdateResearchReport(ResearchReportViewModel reportViewModel);
        Task DeleteResearchReport(int researchReportId);
    }
}
