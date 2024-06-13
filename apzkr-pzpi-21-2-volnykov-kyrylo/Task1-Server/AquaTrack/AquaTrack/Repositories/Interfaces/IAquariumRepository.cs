using AquaTrack.Models;

namespace AquaTrack.Repositories.Interfaces
{
    public interface IAquariumRepository
    {
        Task<List<Aquarium>> GetAllAquariumsAsync();
        Task<Aquarium> GetAquariumByIdAsync(int aquariumId);
        Task<Aquarium> AddAquariumAsync(Aquarium aquarium);
        Task<Aquarium> UpdateAquariumAsync(Aquarium aquarium);
        Task DeleteAquariumAsync(int aquariumId);
        Task<List<Aquarium>> GetAquariumsByUserIdAsync(int userId);
        Task<Aquarium> GetAquariumByTypeAsync(string aquariumType);

    }
}
