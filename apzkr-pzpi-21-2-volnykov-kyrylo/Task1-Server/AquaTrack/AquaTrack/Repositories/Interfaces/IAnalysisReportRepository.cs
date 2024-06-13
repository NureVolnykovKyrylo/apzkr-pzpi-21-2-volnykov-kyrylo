using AquaTrack.Models;

namespace AquaTrack.Repositories.Interfaces
{
    public interface IAnalysisReportRepository
    {
        Task<List<AnalysisReport>> GetAllAnalysisReportsAsync();
        Task<AnalysisReport> GetAnalysisReportByIdAsync(int analysisReportId);
        Task<AnalysisReport> AddAnalysisReportAsync(AnalysisReport analysisReport);
        Task<AnalysisReport> UpdateAnalysisReportAsync(AnalysisReport analysisReport);
        Task DeleteAnalysisReportAsync(int analysisReportId);
    }
}
