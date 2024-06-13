using AquaTrack.ViewModels;

namespace AquaTrack.Services.Interfaces
{
    public interface IAquariumService
    {
        Task<List<AquariumViewModel>> GetAquariumsForUser(int userId);
        Task<List<AquariumViewModel>> GetAquariumsForCurrentUser();
        Task<AquariumViewModel> GetAquariumById(int aquariumId);
        Task<AquariumViewModel> AddAquariumForUser(AquariumViewModel aquariumViewModel, int userId);
        Task<AquariumViewModel> UpdateAquariumForUser(AquariumViewModel aquariumViewModel, int userId);
        Task<bool> DeleteAquariumForUser(int aquariumId, int userId);
        Task<AquariumViewModel> GetAquariumByType(string aquariumType);

    }
}
