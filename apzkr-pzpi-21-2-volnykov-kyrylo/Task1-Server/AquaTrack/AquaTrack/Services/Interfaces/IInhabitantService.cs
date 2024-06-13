using AquaTrack.ViewModels;

namespace AquaTrack.Services.Interfaces
{
    public interface IInhabitantService
    {
        Task<List<InhabitantViewModel>> GetInhabitantsByAquariumId(int aquariumId);
        Task<InhabitantViewModel> GetInhabitantById(int inhabitantId);
        Task<InhabitantViewModel> AddInhabitantForCurrentUser(InhabitantViewModel inhabitantViewModel);
        Task<InhabitantViewModel> UpdateInhabitantForCurrentUser(InhabitantViewModel inhabitantViewModel);
        Task<bool> DeleteInhabitantForCurrentUser(int inhabitantId);
    }
}
