using AquaTrack.Models;
using AquaTrack.Repositories;
using AquaTrack.Repositories.Interfaces;
using AquaTrack.Services.Interfaces;
using AquaTrack.ViewModels;
using AutoMapper;

namespace AquaTrack.Services
{
    public class InhabitantService : IInhabitantService
    {
        private readonly IAuthentificationService _authService;
        private readonly IInhabitantRepository _inhabitantRepository;
        private readonly IAquariumRepository _aquariumRepository;
        private readonly IMapper _mapper;

        public InhabitantService(
            IAuthentificationService authService,
            IInhabitantRepository inhabitantRepository,
            IAquariumRepository aquariumRepository,
            IMapper mapper)
        {
            _authService = authService;
            _aquariumRepository = aquariumRepository;
            _inhabitantRepository = inhabitantRepository;
            _mapper = mapper;
        }

        public async Task<List<InhabitantViewModel>> GetInhabitantsByAquariumId(int aquariumId)
        {
            //var user = await _authService.GetCurrentUser();
            //if (user == null)
            //{
            //    return null;
            //}

            var inhabitants = await _inhabitantRepository.GetInhabitantsByAquariumIdAsync(aquariumId);
            var inhabitantViewModels = _mapper.Map<List<InhabitantViewModel>>(inhabitants);
            return inhabitantViewModels;
        }

        public async Task<InhabitantViewModel> GetInhabitantById(int inhabitantId)
        {
            var inhabitant = await _inhabitantRepository.GetInhabitantByIdAsync(inhabitantId);
            if (inhabitant == null)
            {
                return null;
            }

            var inhabitantViewModel = _mapper.Map<InhabitantViewModel>(inhabitant);
            return inhabitantViewModel;
        }

        public async Task<InhabitantViewModel> AddInhabitantForCurrentUser(InhabitantViewModel inhabitantViewModel)
        {
            //var user = await _authService.GetCurrentUser();
            //if (user == null)
            //{
            //    return null;
            //}

            var aquarium = await _aquariumRepository.GetAquariumByIdAsync(inhabitantViewModel.AquariumId);
            if (aquarium == null /*|| aquarium.UserId != user.UserId*/)
            {
                return null; // The user doesn't have access to the specified aquarium
            }

            var inhabitant = _mapper.Map<Inhabitant>(inhabitantViewModel);

            var addedInhabitant = await _inhabitantRepository.AddInhabitantAsync(inhabitant);
            var addedInhabitantViewModel = _mapper.Map<InhabitantViewModel>(addedInhabitant);
            return addedInhabitantViewModel;
        }

        public async Task<InhabitantViewModel> UpdateInhabitantForCurrentUser(InhabitantViewModel inhabitantViewModel)
        {
            //var user = await _authService.GetCurrentUser();
            //if (user == null)
            //{
            //    return null;
            //}

            var inhabitant = await _inhabitantRepository.GetInhabitantByIdAsync(inhabitantViewModel.InhabitantId);
            if (inhabitant == null /*|| inhabitant.Aquarium.UserId != user.UserId*/)
            {
                return null;
            }

            _mapper.Map(inhabitantViewModel, inhabitant);
            var updatedInhabitant = await _inhabitantRepository.UpdateInhabitantAsync(inhabitant);
            var updatedInhabitantViewModel = _mapper.Map<InhabitantViewModel>(updatedInhabitant);
            return updatedInhabitantViewModel;
        }

        public async Task<bool> DeleteInhabitantForCurrentUser(int inhabitantId)
        {
            //var user = await _authService.GetCurrentUser();
            //if (user == null)
            //{
            //    return false;
            //}

            var inhabitant = await _inhabitantRepository.GetInhabitantByIdAsync(inhabitantId);
            if (inhabitant == null /*|| inhabitant.Aquarium.UserId != user.UserId*/)
            {
                return false;
            }

            await _inhabitantRepository.DeleteInhabitantAsync(inhabitantId);
            return true;
        }
    }
}
