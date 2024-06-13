using AquaTrack.Database;
using AquaTrack.Models;
using AquaTrack.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AquaTrack.Repositories
{
    public class AquariumRepository : IAquariumRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AquariumRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Aquarium>> GetAllAquariumsAsync()
        {
            return await _dbContext.Aquariums
                .Include(a => a.User)
                .Include(a => a.Inhabitants)
                .Include(a => a.FeedingSchedules)
                .ToListAsync();
        }

        public async Task<Aquarium> GetAquariumByIdAsync(int aquariumId)
        {
            return await _dbContext.Aquariums
                .Include(a => a.User)
                .Include(a => a.Inhabitants)
                .Include(a => a.FeedingSchedules)
                .FirstOrDefaultAsync(a => a.AquariumId == aquariumId);
        }

        public async Task<Aquarium> AddAquariumAsync(Aquarium aquarium)
        {
            _dbContext.Aquariums.Add(aquarium);
            await _dbContext.SaveChangesAsync();
            return aquarium;
        }

        public async Task<Aquarium> UpdateAquariumAsync(Aquarium aquarium)
        {
            _dbContext.Entry(aquarium).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return aquarium;
        }

        public async Task DeleteAquariumAsync(int aquariumId)
        {
            var aquarium = await _dbContext.Aquariums.FindAsync(aquariumId);
            if (aquarium != null)
            {
                _dbContext.Aquariums.Remove(aquarium);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Aquarium>> GetAquariumsByUserIdAsync(int userId)
        {
            return await _dbContext.Aquariums
                .Where(a => a.UserId == userId)
                .ToListAsync();
        }

        public async Task<Aquarium> GetAquariumByTypeAsync(string aquariumType)
        {
            return await _dbContext.Aquariums
                .FirstOrDefaultAsync(a => a.AquariumType == aquariumType);
        }
    }
}
