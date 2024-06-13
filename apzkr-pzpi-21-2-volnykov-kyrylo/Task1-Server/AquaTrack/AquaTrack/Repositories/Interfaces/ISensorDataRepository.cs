using AquaTrack.Models;

namespace AquaTrack.Repositories.Interfaces
{
    public interface ISensorDataRepository
    {
        Task<List<SensorData>> GetAllSensorDataAsync();
        Task<SensorData> GetSensorDataByIdAsync(int sensorDataId);
        Task<SensorData> AddSensorDataAsync(SensorData sensorData);
        Task<SensorData> UpdateSensorDataAsync(SensorData sensorData);
        Task DeleteSensorDataAsync(int sensorDataId);

        Task<SensorData> FindSensorBySensorIdentificatorAndTypeAsync(string sensorIdentificator, SensorType sensorType);
    }
}
