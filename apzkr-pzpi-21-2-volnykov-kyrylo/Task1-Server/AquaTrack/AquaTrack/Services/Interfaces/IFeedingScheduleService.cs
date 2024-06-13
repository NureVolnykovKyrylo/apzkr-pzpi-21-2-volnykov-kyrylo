using AquaTrack.ViewModels;

namespace AquaTrack.Services.Interfaces
{
    public interface IFeedingScheduleService
    {
        Task<List<FeedingScheduleViewModel>> GetFeedingSchedulesForCurrentUser();
        Task<FeedingScheduleViewModel> GetFeedingScheduleById(int feedingScheduleId);
        Task<FeedingScheduleViewModel> AddFeedingScheduleForCurrentUser(FeedingScheduleViewModel feedingScheduleViewModel);
        Task<FeedingScheduleViewModel> UpdateFeedingScheduleForCurrentUser(FeedingScheduleViewModel feedingScheduleViewModel);
        Task<bool> DeleteFeedingScheduleForCurrentUser(int feedingScheduleId);

        Task<List<FeedingScheduleViewModel>> GetFeedingSchedulesByAquariumId(int aquariumId);
    }
}
