using Microsoft.EntityFrameworkCore;
using AquaTrack.Database;
using AquaTrack.Models;
using AquaTrack.Repositories.Interfaces;

namespace AquaTrack.Repositories
{
    public class SensorDataRepository : ISensorDataRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SensorDataRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<SensorData>> GetAllSensorDataAsync()
        {
            return await _dbContext.SensorData.Include(s => s.ResearchReports).ToListAsync();
        }

        public async Task<SensorData> GetSensorDataByIdAsync(int sensorDataId)
        {
            return await _dbContext.SensorData.Include(s => s.ResearchReports)
                                               .FirstOrDefaultAsync(s => s.SensorDataId == sensorDataId);
        }

        public async Task<SensorData> AddSensorDataAsync(SensorData sensorData)
        {
            _dbContext.SensorData.Add(sensorData);
            await _dbContext.SaveChangesAsync();
            return sensorData;
        }

        public async Task<SensorData> UpdateSensorDataAsync(SensorData sensorData)
        {
            var sensor = await _dbContext.SensorData.FindAsync(sensorData.SensorDataId);
            if (sensor == null)
            {
                return null;
            }

            sensor.SensorValue = sensorData.SensorValue;
            sensor.Timestamp = sensorData.Timestamp;
            sensor.SensorStatus = sensorData.SensorStatus;

            await _dbContext.SaveChangesAsync();
            return sensor;
        }

        public async Task DeleteSensorDataAsync(int sensorDataId)
        {
            var sensorData = await _dbContext.SensorData.FindAsync(sensorDataId);
            if (sensorData != null)
            {
                _dbContext.SensorData.Remove(sensorData);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<SensorData> FindSensorBySensorIdentificatorAndTypeAsync(string sensorIdentificator, SensorType sensorType)
        {
            return await _dbContext.SensorData
                .FirstOrDefaultAsync(s => s.SensorIdentificator == sensorIdentificator && s.SensorType == sensorType);
        }
    }
}
