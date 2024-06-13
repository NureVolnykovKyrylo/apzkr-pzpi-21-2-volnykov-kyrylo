using AquaTrack.Models;

namespace AquaTrack.Repositories.Interfaces
{
    public interface IInhabitantRepository
    {
        Task<List<Inhabitant>> GetAllInhabitantsAsync();
        Task<Inhabitant> GetInhabitantByIdAsync(int inhabitantId);
        Task<Inhabitant> AddInhabitantAsync(Inhabitant inhabitant);
        Task<Inhabitant> UpdateInhabitantAsync(Inhabitant inhabitant);
        Task DeleteInhabitantAsync(int inhabitantId);
        Task<List<Inhabitant>> GetInhabitantsByAquariumIdAsync(int aquariumId);
    }
}
