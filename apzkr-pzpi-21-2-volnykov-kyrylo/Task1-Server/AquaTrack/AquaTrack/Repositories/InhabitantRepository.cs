using AquaTrack.Database;
using AquaTrack.Models;
using AquaTrack.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AquaTrack.Repositories
{
    public class InhabitantRepository : IInhabitantRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public InhabitantRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Inhabitant>> GetAllInhabitantsAsync()
        {
            return await _dbContext.Inhabitants
                .Include(i => i.Aquarium)
                .ToListAsync();
        }

        public async Task<Inhabitant> GetInhabitantByIdAsync(int inhabitantId)
        {
            return await _dbContext.Inhabitants
                .Include(i => i.Aquarium)
                .FirstOrDefaultAsync(i => i.InhabitantId == inhabitantId);
        }

        public async Task<Inhabitant> AddInhabitantAsync(Inhabitant inhabitant)
        {
            _dbContext.Inhabitants.Add(inhabitant);
            await _dbContext.SaveChangesAsync();
            return inhabitant;
        }

        public async Task<Inhabitant> UpdateInhabitantAsync(Inhabitant inhabitant)
        {
            _dbContext.Entry(inhabitant).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return inhabitant;
        }

        public async Task DeleteInhabitantAsync(int inhabitantId)
        {
            var inhabitant = await _dbContext.Inhabitants.FindAsync(inhabitantId);
            if (inhabitant != null)
            {
                _dbContext.Inhabitants.Remove(inhabitant);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Inhabitant>> GetInhabitantsByAquariumIdAsync(int aquariumId)
        {
            return await _dbContext.Inhabitants
                .Where(i => i.AquariumId == aquariumId)
                .Include(i => i.Aquarium)
                .ToListAsync();
        }
    }
}
