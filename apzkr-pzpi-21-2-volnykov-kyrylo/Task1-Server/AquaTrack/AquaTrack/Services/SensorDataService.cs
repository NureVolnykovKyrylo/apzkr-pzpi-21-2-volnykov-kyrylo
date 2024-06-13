using AquaTrack.Models;
using AquaTrack.Repositories.Interfaces;
using AquaTrack.Services.Interfaces;
using AquaTrack.ViewModels;
using AutoMapper;

namespace AquaTrack.Services
{
    public class SensorDataService : ISensorDataService
    {
        private readonly ISensorDataRepository _sensorDataRepository;
        private readonly IMapper _mapper;

        public SensorDataService(ISensorDataRepository sensorDataRepository, IMapper mapper)
        {
            _sensorDataRepository = sensorDataRepository;
            _mapper = mapper;
        }

        public async Task<List<SensorDataViewModel>> GetAllSensorData()
        {
            var sensorData = await _sensorDataRepository.GetAllSensorDataAsync();
            var sensorDataViewModels = _mapper.Map<List<SensorDataViewModel>>(sensorData);
            return sensorDataViewModels;
        }

        public async Task<SensorDataViewModel> GetSensorDataById(int sensorDataId)
        {
            var sensorData = await _sensorDataRepository.GetSensorDataByIdAsync(sensorDataId);
            var sensorDataViewModel = _mapper.Map<SensorDataViewModel>(sensorData);
            return sensorDataViewModel;
        }

        public async Task<SensorDataViewModel> AddSensorData(SensorDataViewModel sensorDataViewModel)
        {
            var sensorData = _mapper.Map<SensorData>(sensorDataViewModel);
            var addedSensorData = await _sensorDataRepository.AddSensorDataAsync(sensorData);
            var addedSensorDataViewModel = _mapper.Map<SensorDataViewModel>(addedSensorData);
            return addedSensorDataViewModel;
        }

        public async Task<SensorDataViewModel> UpdateSensorData(SensorDataUpdateViewModel sensorDataUpdateViewModel)
        {
            var sensorData = _mapper.Map<SensorData>(sensorDataUpdateViewModel);
            var updatedSensorData = await _sensorDataRepository.UpdateSensorDataAsync(sensorData);
            var updatedSensorDataViewModel = _mapper.Map<SensorDataViewModel>(updatedSensorData);
            return updatedSensorDataViewModel;
        }

        public async Task DeleteSensorData(int sensorDataId)
        {
            await _sensorDataRepository.DeleteSensorDataAsync(sensorDataId);
        }

        public async Task<SensorDataViewModel> FindSensorBySensorIdentificatorAndType(string sensorIdentificator, SensorType sensorType)
        {
            var sensorData = await _sensorDataRepository.FindSensorBySensorIdentificatorAndTypeAsync(sensorIdentificator, sensorType);
            var sensorDataViewModel = _mapper.Map<SensorDataViewModel>(sensorData);
            return sensorDataViewModel;
        }

    }
}
