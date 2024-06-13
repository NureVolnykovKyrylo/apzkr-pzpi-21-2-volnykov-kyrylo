using AquaTrack.Models;

namespace AquaTrack.Repositories.Interfaces
{
    public interface IResearchReportRepository
    {
        Task<List<ResearchReport>> GetAllResearchReportsAsync();
        Task<ResearchReport> GetResearchReportByIdAsync(int researchReportId);
        Task<ResearchReport> AddResearchReportAsync(ResearchReport researchReport);
        Task<ResearchReport> UpdateResearchReportAsync(ResearchReport researchReport);
        Task DeleteResearchReportAsync(int researchReportId);
    }
}
