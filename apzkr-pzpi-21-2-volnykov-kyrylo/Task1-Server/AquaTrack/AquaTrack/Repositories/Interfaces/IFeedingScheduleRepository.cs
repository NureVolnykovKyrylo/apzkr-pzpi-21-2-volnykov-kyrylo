using AquaTrack.Models;

namespace AquaTrack.Repositories.Interfaces
{
    public interface IFeedingScheduleRepository
    {
        Task<List<FeedingSchedule>> GetAllFeedingSchedulesAsync();
        Task<FeedingSchedule> GetFeedingScheduleByIdAsync(int feedingScheduleId);
        Task<FeedingSchedule> AddFeedingScheduleAsync(FeedingSchedule feedingSchedule);
        Task<FeedingSchedule> UpdateFeedingScheduleAsync(FeedingSchedule feedingSchedule);
        Task DeleteFeedingScheduleAsync(int feedingScheduleId);
        Task<List<FeedingSchedule>> GetFeedingSchedulesByAquariumIdAsync(int aquariumId);

        Task<List<FeedingSchedule>> GetFeedingSchedulesByUserIdAsync(int userId);
    }
}
