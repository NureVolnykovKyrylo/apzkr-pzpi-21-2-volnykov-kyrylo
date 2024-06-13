using AquaTrack.Models;
using AquaTrack.ViewModels;

namespace AquaTrack.Services.Interfaces
{
    public interface ISensorDataService
    {
        Task<List<SensorDataViewModel>> GetAllSensorData();
        Task<SensorDataViewModel> GetSensorDataById(int sensorDataId);
        Task<SensorDataViewModel> AddSensorData(SensorDataViewModel sensorDataViewModel);
        Task<SensorDataViewModel> UpdateSensorData(SensorDataUpdateViewModel sensorDataViewModel);
        Task DeleteSensorData(int sensorDataId);
        Task<SensorDataViewModel> FindSensorBySensorIdentificatorAndType(string sensorIdentificator, SensorType sensorType);
    }
}
