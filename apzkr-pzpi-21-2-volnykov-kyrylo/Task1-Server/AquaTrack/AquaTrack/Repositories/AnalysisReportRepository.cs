using AquaTrack.Database;
using AquaTrack.Models;
using AquaTrack.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AquaTrack.Repositories
{
    public class AnalysisReportRepository : IAnalysisReportRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AnalysisReportRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<AnalysisReport>> GetAllAnalysisReportsAsync()
        {
            return await _dbContext.AnalysisReports.Include(a => a.ResearchReport).ToListAsync();
        }

        public async Task<AnalysisReport> GetAnalysisReportByIdAsync(int analysisReportId)
        {
            return await _dbContext.AnalysisReports.Include(a => a.ResearchReport)
                                                   .FirstOrDefaultAsync(a => a.AnalysisReportId == analysisReportId);
        }

        public async Task<AnalysisReport> AddAnalysisReportAsync(AnalysisReport analysisReport)
        {
            _dbContext.AnalysisReports.Add(analysisReport);
            await _dbContext.SaveChangesAsync();
            return analysisReport;
        }

        public async Task<AnalysisReport> UpdateAnalysisReportAsync(AnalysisReport analysisReport)
        {
            _dbContext.Entry(analysisReport).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return analysisReport;
        }

        public async Task DeleteAnalysisReportAsync(int analysisReportId)
        {
            var analysisReport = await _dbContext.AnalysisReports.FindAsync(analysisReportId);
            if (analysisReport != null)
            {
                _dbContext.AnalysisReports.Remove(analysisReport);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
