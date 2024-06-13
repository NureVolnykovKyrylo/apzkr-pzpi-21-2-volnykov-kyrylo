using AquaTrack.ViewModels;

namespace AquaTrack.Services.Interfaces
{
    public interface IAnalysisReportService
    {
        Task<List<AnalysisReportViewModel>> GetAllAnalysisReports();
        Task<AnalysisReportViewModel> GetAnalysisReportById(int analysisReportId);
        Task<AnalysisReportViewModel> UpdateAnalysisReport(AnalysisReportViewModel analysisReportViewModel);
        Task DeleteAnalysisReport(int analysisReportId);
        Task<AnalysisReportViewModel> AddAnalysisReport(int researchReportId);
    }
}
