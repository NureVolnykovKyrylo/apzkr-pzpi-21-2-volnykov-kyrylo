using AquaTrack.Models;
using AquaTrack.Services.Interfaces;
using AquaTrack.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AquaTrack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensorDataController : ControllerBase
    {
        private readonly ISensorDataService _sensorDataService;

        public SensorDataController(ISensorDataService sensorDataService)
        {
            _sensorDataService = sensorDataService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SensorDataViewModel>>> GetAllSensorData()
        {
            var sensorData = await _sensorDataService.GetAllSensorData();
            if (sensorData == null)
            {
                return NotFound();
            }

            return sensorData;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SensorDataViewModel>> GetSensorDataById(int id)
        {
            var sensorData = await _sensorDataService.GetSensorDataById(id);
            if (sensorData == null)
            {
                return NotFound();
            }

            return sensorData;
        }

        [HttpPost]
        public async Task<ActionResult<SensorDataViewModel>> AddSensorData(SensorDataViewModel sensorDataViewModel)
        {
            var addedSensorData = await _sensorDataService.AddSensorData(sensorDataViewModel);
            if (addedSensorData == null)
            {
                return BadRequest();
            }

            return addedSensorData;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SensorDataViewModel>> UpdateSensorData(int id, SensorDataUpdateViewModel sensorDataUpdateViewModel)
        {
            if (id != sensorDataUpdateViewModel.SensorDataId)
            {
                return BadRequest();
            }

            var updatedSensorData = await _sensorDataService.UpdateSensorData(sensorDataUpdateViewModel);
            if (updatedSensorData == null)
            {
                return NotFound();
            }

            return updatedSensorData;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSensorData(int id)
        {
            await _sensorDataService.DeleteSensorData(id);
            return NoContent();
        }

        [HttpGet("find")]
        public async Task<ActionResult<SensorDataViewModel>> FindSensorBySensorIdentificatorAndType(string sensorIdentificator, SensorType sensorType)
        {
            var sensorData = await _sensorDataService.FindSensorBySensorIdentificatorAndType(sensorIdentificator, sensorType);
            if (sensorData == null)
            {
                return NotFound();
            }

            return sensorData;
        }
    }
}
