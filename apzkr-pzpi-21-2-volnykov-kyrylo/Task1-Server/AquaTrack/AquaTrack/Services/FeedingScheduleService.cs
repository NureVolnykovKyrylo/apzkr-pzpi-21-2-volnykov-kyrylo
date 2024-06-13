using AquaTrack.Models;
using AquaTrack.Repositories.Interfaces;
using AquaTrack.Services.Interfaces;
using AquaTrack.ViewModels;
using AutoMapper;

namespace AquaTrack.Services
{
    public class FeedingScheduleService : IFeedingScheduleService
    {
        private readonly IAuthentificationService _authService;
        private readonly IFeedingScheduleRepository _feedingScheduleRepository;
        private readonly IAquariumRepository _aquariumRepository;
        private readonly IMapper _mapper;

        public FeedingScheduleService(
            IAuthentificationService authService,
            IFeedingScheduleRepository feedingScheduleRepository,
            IAquariumRepository aquariumRepository,
            IMapper mapper)
        {
            _authService = authService;
            _feedingScheduleRepository = feedingScheduleRepository;
            _aquariumRepository = aquariumRepository;
            _mapper = mapper;
        }

        public async Task<List<FeedingScheduleViewModel>> GetFeedingSchedulesForCurrentUser()
        {
            var user = await _authService.GetCurrentUser();
            if (user == null)
            {
                return null;
            }

            var feedingSchedules = await _feedingScheduleRepository.GetFeedingSchedulesByUserIdAsync(user.UserId);
            var feedingScheduleViewModels = _mapper.Map<List<FeedingScheduleViewModel>>(feedingSchedules);
            return feedingScheduleViewModels;
        }

        public async Task<List<FeedingScheduleViewModel>> GetFeedingSchedulesByAquariumId(int aquariumId)
        {
            var feedingSchedules = await _feedingScheduleRepository.GetFeedingSchedulesByAquariumIdAsync(aquariumId);
            var feedingScheduleViewModels = _mapper.Map<List<FeedingScheduleViewModel>>(feedingSchedules);
            return feedingScheduleViewModels;
        }

        public async Task<FeedingScheduleViewModel> GetFeedingScheduleById(int feedingScheduleId)
        {
            var feedingSchedule = await _feedingScheduleRepository.GetFeedingScheduleByIdAsync(feedingScheduleId);
            if (feedingSchedule == null)
            {
                return null;
            }

            var feedingScheduleViewModel = _mapper.Map<FeedingScheduleViewModel>(feedingSchedule);
            return feedingScheduleViewModel;
        }

        public async Task<FeedingScheduleViewModel> AddFeedingScheduleForCurrentUser(FeedingScheduleViewModel feedingScheduleViewModel)
        {
            //var user = await _authService.GetCurrentUser();
            //if (user == null)
            //{
            //    return null;
            //}

            var aquarium = await _aquariumRepository.GetAquariumByIdAsync(feedingScheduleViewModel.AquariumId);
            if (aquarium == null /*|| aquarium.UserId != user.UserId*/)
            {
                return null; // The user doesn't have access to the specified aquarium
            }

            var feedingSchedule = _mapper.Map<FeedingSchedule>(feedingScheduleViewModel);

            var addedFeedingSchedule = await _feedingScheduleRepository.AddFeedingScheduleAsync(feedingSchedule);
            var addedFeedingScheduleViewModel = _mapper.Map<FeedingScheduleViewModel>(addedFeedingSchedule);
            return addedFeedingScheduleViewModel;
        }

        public async Task<FeedingScheduleViewModel> UpdateFeedingScheduleForCurrentUser(FeedingScheduleViewModel feedingScheduleViewModel)
        {
            //var user = await _authService.GetCurrentUser();
            //if (user == null)
            //{
            //    return null;
            //}

            var feedingSchedule = await _feedingScheduleRepository.GetFeedingScheduleByIdAsync(feedingScheduleViewModel.FeedingScheduleId);
            if (feedingSchedule == null /*|| feedingSchedule.Aquarium.UserId != user.UserId*/)
            {
                return null;
            }

            _mapper.Map(feedingScheduleViewModel, feedingSchedule);
            var updatedFeedingSchedule = await _feedingScheduleRepository.UpdateFeedingScheduleAsync(feedingSchedule);
            var updatedFeedingScheduleViewModel = _mapper.Map<FeedingScheduleViewModel>(updatedFeedingSchedule);
            return updatedFeedingScheduleViewModel;
        }

        public async Task<bool> DeleteFeedingScheduleForCurrentUser(int feedingScheduleId)
        {
            //var user = await _authService.GetCurrentUser();
            //if (user == null)
            //{
            //    return false;
            //}

            var feedingSchedule = await _feedingScheduleRepository.GetFeedingScheduleByIdAsync(feedingScheduleId);
            if (feedingSchedule == null /*|| feedingSchedule.Aquarium.UserId != user.UserId*/)
            {
                return false;
            }

            await _feedingScheduleRepository.DeleteFeedingScheduleAsync(feedingScheduleId);
            return true;
        }
    }
}
