using AquaTrack.Models;
using AquaTrack.Repositories.Interfaces;
using AquaTrack.Services.Interfaces;
using AquaTrack.Utils;
using AquaTrack.ViewModels;
using AutoMapper;

namespace AquaTrack.Services
{
    public class AquariumService : IAquariumService
    {
        private readonly IAuthentificationService _authService;
        private readonly IAquariumRepository _aquariumRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AquariumService(
            IAuthentificationService authService,
            IAquariumRepository aquariumRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _authService = authService;
            _aquariumRepository = aquariumRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AquariumViewModel> GetAquariumById(int aquariumId)
        {
            var aquarium = await _aquariumRepository.GetAquariumByIdAsync(aquariumId);
            if (aquarium == null)
            {
                return null;
            }

            var aquariumViewModel = _mapper.Map<AquariumViewModel>(aquarium);
            return aquariumViewModel;
        }

        public async Task<List<AquariumViewModel>> GetAquariumsForCurrentUser()
        {
            var user = await _authService.GetCurrentUser();
            if (user == null)
            {
                return null;
            }

            var aquariums = await _aquariumRepository.GetAquariumsByUserIdAsync(user.UserId);
            var aquariumViewModels = _mapper.Map<List<AquariumViewModel>>(aquariums);
            return aquariumViewModels;
        }

        public async Task<List<AquariumViewModel>> GetAquariumsForUser(int userId)
        {
            var currentUserRole = UserAccessUtil.GetCurrentUserRole(_httpContextAccessor);
            //if (currentUserRole != Role.Admin)
            //{
            //    return null;
            //}

            var aquariums = await _aquariumRepository.GetAquariumsByUserIdAsync(userId);
            var aquariumViewModels = _mapper.Map<List<AquariumViewModel>>(aquariums);
            return aquariumViewModels;
        }

        public async Task<AquariumViewModel> AddAquariumForUser(AquariumViewModel aquariumViewModel, int userId)
        {
            var currentUserRole = UserAccessUtil.GetCurrentUserRole(_httpContextAccessor);
            //if (currentUserRole != Role.Admin)
            //{
            //    return null;
            //}

            var aquarium = _mapper.Map<Aquarium>(aquariumViewModel);
            aquarium.UserId = userId;

            var addedAquarium = await _aquariumRepository.AddAquariumAsync(aquarium);
            var addedAquariumViewModel = _mapper.Map<AquariumViewModel>(addedAquarium);
            return addedAquariumViewModel;
        }

        public async Task<AquariumViewModel> UpdateAquariumForUser(AquariumViewModel aquariumViewModel, int userId)
        {
            var currentUserRole = UserAccessUtil.GetCurrentUserRole(_httpContextAccessor);
            //if (currentUserRole != Role.Admin)
            //{
            //    return null;
            //}

            var aquarium = await _aquariumRepository.GetAquariumByIdAsync(aquariumViewModel.AquariumId);
            if (aquarium == null || aquarium.UserId != userId)
            {
                return null;
            }

            _mapper.Map(aquariumViewModel, aquarium);
            var updatedAquarium = await _aquariumRepository.UpdateAquariumAsync(aquarium);
            var updatedAquariumViewModel = _mapper.Map<AquariumViewModel>(updatedAquarium);
            return updatedAquariumViewModel;
        }

        public async Task<bool> DeleteAquariumForUser(int aquariumId, int userId)
        {
            var currentUserRole = UserAccessUtil.GetCurrentUserRole(_httpContextAccessor);
            //if (currentUserRole != Role.Admin)
            //{
            //    return false;
            //}

            var aquarium = await _aquariumRepository.GetAquariumByIdAsync(aquariumId);
            if (aquarium == null || aquarium.UserId != userId)
            {
                return false;
            }
            await _aquariumRepository.DeleteAquariumAsync(aquariumId);
            return true;
        }

        public async Task<AquariumViewModel> GetAquariumByType(string aquariumType)
        {
            var aquarium = await _aquariumRepository.GetAquariumByTypeAsync(aquariumType);
            if (aquarium == null)
            {
                return null;
            }

            var aquariumViewModel = _mapper.Map<AquariumViewModel>(aquarium);
            return aquariumViewModel;
        }
    }
}