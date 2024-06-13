using AquaTrack.Database;
using AquaTrack.Models;
using AquaTrack.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AquaTrack.Repositories
{
    public class ResearchReportRepository : IResearchReportRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ResearchReportRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ResearchReport>> GetAllResearchReportsAsync()
        {
            return await _dbContext.ResearchReports
                .Include(r => r.AnalysisReport)
                .Include(r => r.Aquarium)
                .Include(r => r.SensorData)
                .ToListAsync();
        }

        public async Task<ResearchReport> GetResearchReportByIdAsync(int researchReportId)
        {
            return await _dbContext.ResearchReports
                .Include(r => r.AnalysisReport)
                .Include(r => r.Aquarium)
                .Include(r => r.SensorData)
                .FirstOrDefaultAsync(r => r.ResearchReportId == researchReportId);
        }

        public async Task<ResearchReport> AddResearchReportAsync(ResearchReport researchReport)
        {
            researchReport.AnalysisReport = await _dbContext.AnalysisReports
                .FirstOrDefaultAsync(a => a.AnalysisReportId == researchReport.AnalysisReportId);

            researchReport.Aquarium = await _dbContext.Aquariums
                .FirstOrDefaultAsync(a => a.AquariumId == researchReport.AquariumId);

            for (int i = 0; i < researchReport.SensorData.Count; i++)
            {
                researchReport.SensorData[i] = await _dbContext.SensorData
                    .FirstOrDefaultAsync(a => a.SensorDataId == researchReport.SensorData[i].SensorDataId);
            }

            _dbContext.ResearchReports.Add(researchReport);
            await _dbContext.SaveChangesAsync();

            return researchReport;
        }

        public async Task<ResearchReport> UpdateResearchReportAsync(ResearchReport researchReport)
        {
            _dbContext.Entry(researchReport).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return researchReport;
        }

        public async Task DeleteResearchReportAsync(int researchReportId)
        {
            var researchReport = await _dbContext.ResearchReports.FindAsync(researchReportId);
            if (researchReport != null)
            {
                _dbContext.ResearchReports.Remove(researchReport);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
