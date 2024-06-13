using AquaTrack.Database;
using AquaTrack.Models;
using AquaTrack.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AquaTrack.Repositories
{
    public class FeedingScheduleRepository : IFeedingScheduleRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IAquariumRepository _aquariumRepository;

        public FeedingScheduleRepository(ApplicationDbContext dbContext, IAquariumRepository aquariumRepository)
        {
            _dbContext = dbContext;
            _aquariumRepository = aquariumRepository;
        }

        public async Task<List<FeedingSchedule>> GetAllFeedingSchedulesAsync()
        {
            return await _dbContext.FeedingSchedules
                .Include(fs => fs.Aquarium)
                .ToListAsync();
        }

        public async Task<FeedingSchedule> GetFeedingScheduleByIdAsync(int feedingScheduleId)
        {
            return await _dbContext.FeedingSchedules
                .Include(fs => fs.Aquarium)
                .FirstOrDefaultAsync(fs => fs.FeedingScheduleId == feedingScheduleId);
        }

        public async Task<FeedingSchedule> AddFeedingScheduleAsync(FeedingSchedule feedingSchedule)
        {
            _dbContext.FeedingSchedules.Add(feedingSchedule);
            await _dbContext.SaveChangesAsync();
            return feedingSchedule;
        }

        public async Task<FeedingSchedule> UpdateFeedingScheduleAsync(FeedingSchedule feedingSchedule)
        {
            _dbContext.Entry(feedingSchedule).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return feedingSchedule;
        }

        public async Task DeleteFeedingScheduleAsync(int feedingScheduleId)
        {
            var feedingSchedule = await _dbContext.FeedingSchedules.FindAsync(feedingScheduleId);
            if (feedingSchedule != null)
            {
                _dbContext.FeedingSchedules.Remove(feedingSchedule);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<FeedingSchedule>> GetFeedingSchedulesByAquariumIdAsync(int aquariumId)
        {
            return await _dbContext.FeedingSchedules
                .Where(fs => fs.AquariumId == aquariumId)
                .Include(fs => fs.Aquarium)
                .ToListAsync();
        }

        public async Task<List<FeedingSchedule>> GetFeedingSchedulesByUserIdAsync(int userId)
        {
            var aquariums = await _aquariumRepository.GetAquariumsByUserIdAsync(userId);
            var aquariumIds = aquariums.Select(a => a.AquariumId).ToList();

            return await _dbContext.FeedingSchedules
                .Where(fs => aquariumIds.Contains(fs.AquariumId))
                .Include(fs => fs.Aquarium)
                .ToListAsync();
        }
    }
}
