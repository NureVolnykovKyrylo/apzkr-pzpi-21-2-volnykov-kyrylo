using AutoMapper;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AquaTrack.Models;
using AquaTrack.Repositories.Interfaces;
using AquaTrack.Services.Interfaces;
using AquaTrack.ViewModels;

namespace AquaTrack.Services
{
    public class AnalysisReportService : IAnalysisReportService
    {
        private readonly IAnalysisReportRepository _analysisReportRepository;
        private readonly IResearchReportRepository _researchReportRepository;
        private readonly IAquariumRepository _aquariumRepository;
        private readonly IInhabitantRepository _inhabitantRepository;
        private readonly IMapper _mapper;

        public AnalysisReportService(
            IAnalysisReportRepository analysisReportRepository,
            IResearchReportRepository researchReportRepository,
            IAquariumRepository aquariumRepository,
            IInhabitantRepository inhabitantRepository,
            IMapper mapper)
        {
            _analysisReportRepository = analysisReportRepository;
            _researchReportRepository = researchReportRepository;
            _aquariumRepository = aquariumRepository;
            _inhabitantRepository = inhabitantRepository;
            _mapper = mapper;
        }

        public async Task<List<AnalysisReportViewModel>> GetAllAnalysisReports()
        {
            var analysisReports = await _analysisReportRepository.GetAllAnalysisReportsAsync();
            var analysisReportViewModels = _mapper.Map<List<AnalysisReportViewModel>>(analysisReports);
            return analysisReportViewModels;
        }

        public async Task<AnalysisReportViewModel> GetAnalysisReportById(int analysisReportId)
        {
            var analysisReport = await _analysisReportRepository.GetAnalysisReportByIdAsync(analysisReportId);
            var analysisReportViewModel = _mapper.Map<AnalysisReportViewModel>(analysisReport);
            return analysisReportViewModel;
        }

        public async Task<AnalysisReportViewModel> UpdateAnalysisReport(AnalysisReportViewModel analysisReportViewModel)
        {
            var analysisReport = _mapper.Map<AnalysisReport>(analysisReportViewModel);
            var updatedAnalysisReport = await _analysisReportRepository.UpdateAnalysisReportAsync(analysisReport);
            var updatedAnalysisReportViewModel = _mapper.Map<AnalysisReportViewModel>(updatedAnalysisReport);
            return updatedAnalysisReportViewModel;
        }

        public async Task DeleteAnalysisReport(int analysisReportId)
        {
            await _analysisReportRepository.DeleteAnalysisReportAsync(analysisReportId);
        }

        public async Task<AnalysisReportViewModel> AddAnalysisReport(int researchReportId)
        {
            // Retrieve research report data needed for analysis
            var researchReport = await _researchReportRepository.GetResearchReportByIdAsync(researchReportId);
            if (researchReport == null)
            {
                return null;
            }


            // Retrieve sensor data from research report needed for analysis
            var sensorData = researchReport.SensorData;
            if (sensorData == null || sensorData.Count == 0)
            {
                return null;
            }

            // Retrieve aquarium information for analysis
            var aquarium = await _aquariumRepository.GetAquariumByIdAsync(researchReport.AquariumId);
            if (researchReport == null)
            {
                return null;
            }
            var aquariumViewModel = _mapper.Map<AquariumViewModel>(aquarium);

            // Initialize dictionaries to store sensor data grouped by sensor type
            var movementSensorDataMap = new Dictionary<string, List<SensorData>>();
            var videoSensorDataMap = new Dictionary<string, List<SensorData>>();

            var temperatureSensorDataMap = new Dictionary<string, List<SensorData>>();
            var waterLevelSensorDataMap = new Dictionary<string, List<SensorData>>();
            var aciditySensorDataMap = new Dictionary<string, List<SensorData>>();
            var lightingSensorDataMap = new Dictionary<string, List<SensorData>>();

            // Group sensor data based on sensor type
            foreach (var sensor in sensorData)
            {
                switch (sensor.SensorType)
                {
                    case SensorType.MovementSensor when aquarium.Inhabitants.Any(a => a.Name == sensor.SensorIdentificator):
                        AddToSensorDataMap(movementSensorDataMap, sensor.SensorIdentificator, sensor);
                        break;

                    case SensorType.VideoSensor when aquarium.Inhabitants.Any(a => a.Name == sensor.SensorIdentificator):
                        AddToSensorDataMap(videoSensorDataMap, sensor.SensorIdentificator, sensor);
                        break;

                    case SensorType.TemperatureSensor:
                        if (sensor.SensorIdentificator == aquarium.AquariumType)
                        {
                            AddToSensorDataMap(temperatureSensorDataMap, sensor.SensorIdentificator, sensor);
                        }
                        break;
                    case SensorType.WaterLevelSensor:
                        if (sensor.SensorIdentificator == aquarium.AquariumType)
                        {
                            AddToSensorDataMap(waterLevelSensorDataMap, sensor.SensorIdentificator, sensor);
                        }
                        break;
                    case SensorType.AciditySensor:
                        if (sensor.SensorIdentificator == aquarium.AquariumType)
                        {
                            AddToSensorDataMap(aciditySensorDataMap, sensor.SensorIdentificator, sensor);
                        }
                        break;
                    case SensorType.LightingSensor:
                        if (sensor.SensorIdentificator == aquarium.AquariumType)
                        {
                            AddToSensorDataMap(lightingSensorDataMap, sensor.SensorIdentificator, sensor);
                        }
                        break;

                    default:
                        break;
                }
            }

            var analysedTrends = "";

            // Perform analysis and generate indicators for different aspects
            var movementIndicators = AnalyzeInhabitantActivity(movementSensorDataMap, aquarium.Inhabitants);
            var conditionIndicators = AnalyzeInhabitantCondition(videoSensorDataMap, aquarium.Inhabitants);

            var temperatureIndicators = AnalyzeAquariumTemperature(temperatureSensorDataMap, aquariumViewModel);
            var waterLevelIndicators = AnalyzeAquariumWaterLevel(waterLevelSensorDataMap, aquariumViewModel);
            var acidityIndicators = AnalyzeAquariumAcidity(aciditySensorDataMap, aquariumViewModel);
            var lightingIndicators = AnalyzeAquariumLighting(lightingSensorDataMap, aquariumViewModel);

            // Construct conclusions based on the analyzed indicators
            if (movementIndicators.Any())
            {
                analysedTrends += movementIndicators.Last();
                movementIndicators.RemoveAt(movementIndicators.Count - 1);
            }

            if (conditionIndicators.Any())
            {
                analysedTrends += conditionIndicators.Last();
                conditionIndicators.RemoveAt(conditionIndicators.Count - 1);
            }

            if (temperatureIndicators.Any())
            {
                analysedTrends += temperatureIndicators.Last();
                temperatureIndicators.RemoveAt(temperatureIndicators.Count - 1);
            }

            if (waterLevelIndicators.Any())
            {
                analysedTrends += waterLevelIndicators.Last();
                waterLevelIndicators.RemoveAt(waterLevelIndicators.Count - 1);
            }

            if (acidityIndicators.Any())
            {
                analysedTrends += acidityIndicators.Last();
                acidityIndicators.RemoveAt(acidityIndicators.Count - 1);
            }

            if (lightingIndicators.Any())
            {
                analysedTrends += lightingIndicators.Last();
                lightingIndicators.RemoveAt(lightingIndicators.Count - 1);
            }

            // Extract most common indicators for each aspect
            var mostCommonMovementIndicator = movementIndicators.GroupBy(x => x)
                .OrderByDescending(grp => grp.Count())
                .Select(grp => grp.Key)
                .FirstOrDefault();

            var mostCommonConditionIndicator = conditionIndicators.GroupBy(x => x)
                .OrderByDescending(grp => grp.Count())
                .Select(grp => grp.Key)
                .FirstOrDefault();

            var mostCommonTemperatureIndicator = temperatureIndicators.GroupBy(x => x)
                .OrderByDescending(grp => grp.Count())
                .Select(grp => grp.Key)
                .FirstOrDefault();

            var mostCommonWaterLevelIndicator = waterLevelIndicators.GroupBy(x => x)
                .OrderByDescending(grp => grp.Count())
                .Select(grp => grp.Key)
                .FirstOrDefault();

            var mostCommonAcidityIndicator = acidityIndicators.GroupBy(x => x)
                .OrderByDescending(grp => grp.Count())
                .Select(grp => grp.Key)
                .FirstOrDefault();

            var mostCommonLightingIndicator = lightingIndicators.GroupBy(x => x)
                .OrderByDescending(grp => grp.Count())
                .Select(grp => grp.Key)
                .FirstOrDefault();

            var recommendations = "";

            // Generate recommendations based on conclusions
            recommendations += $"Recommendations on inhabitant activity: {GetRecommendationFromConclusion(mostCommonMovementIndicator)} \n";
            recommendations += $"Recommendations on inhabitant condition: {GetRecommendationFromConclusion(mostCommonConditionIndicator)} \n";
            recommendations += $"Recommendations on aquarium temperature: {GetRecommendationFromConclusion(mostCommonTemperatureIndicator)} \n";
            recommendations += $"Recommendations on aquarium acidity: {GetRecommendationFromConclusion(mostCommonWaterLevelIndicator)} \n";
            recommendations += $"Recommendations on aquarium water level: {GetRecommendationFromConclusion(mostCommonAcidityIndicator)} \n";
            recommendations += $"Recommendations on aquarium lighting: {GetRecommendationFromConclusion(mostCommonLightingIndicator)} \n";

            var analysisReport = new AnalysisReport
            {
                ResearchReportId = researchReportId,
                CreationDate = DateTime.UtcNow,
                Title = "Analysis Report",
                IdentifiedTrends = analysedTrends,
                Recommendations = recommendations
            };

            var addedAnalysisReport = await _analysisReportRepository.AddAnalysisReportAsync(analysisReport);
            var addedAnalysisReportViewModel = _mapper.Map<AnalysisReportViewModel>(addedAnalysisReport);
            return addedAnalysisReportViewModel;
        }

        void AddToSensorDataMap(Dictionary<string, List<SensorData>> sensorDataMap, string key, SensorData sensor)
        {
            if (!sensorDataMap.ContainsKey(key))
            {
                sensorDataMap[key] = new List<SensorData>();
            }

            sensorDataMap[key].Add(sensor);
        }

        public string GetRecommendationFromConclusion(string conclusion)
        {
            if (conclusion != null)
            {
                if (conclusion == "decreased")
                {
                    return "There is a significant decline, take action";
                }
                else if (conclusion == "increased")
                {
                    return "Significant growth is observed, take action";
                }
                else if (conclusion == "normal")
                {
                    return "No significant changes observed";
                }
            }
            return "";
        }

        // Analyze methods
        public List<string> AnalyzeInhabitantActivity(Dictionary<string, List<SensorData>> movementSensorDataMap, List<Inhabitant> inhabitants)
        {
            var activityIndicators = new List<string>();
            var analysedTrends = "Inhabitant activity trends: \n";

            foreach (var inhabitant in inhabitants)
            {
                if (movementSensorDataMap.TryGetValue(inhabitant.Name, out var movementSensorData))
                {
                    if (double.TryParse(inhabitant.Behavior, out double behaviorValue))
                    {
                        var averageMovementValue = movementSensorData.Average(s => s.SensorValue);
                        string movementStatus;

                        if (Math.Abs(averageMovementValue - behaviorValue) < 10)
                        {
                            movementStatus = "on normal value";
                            activityIndicators.Add("normal");
                        }
                        else if (Math.Abs(averageMovementValue - behaviorValue) > 10 && averageMovementValue < behaviorValue)
                        {
                            movementStatus = "decreased";
                            activityIndicators.Add("decreased");
                        }
                        else
                        {
                            movementStatus = "increased";
                            activityIndicators.Add("increased");
                        }

                        analysedTrends += $"{inhabitant.Name} activity {movementStatus} based on research analysis. \n";
                    }
                }
            }
            activityIndicators.Add(analysedTrends);
            return activityIndicators;
        }

        public List<string> AnalyzeInhabitantCondition(Dictionary<string, List<SensorData>> videoSensorDataMap, List<Inhabitant> inhabitants)
        {
            var conditionIndicators = new List<string>();

            var analysedTrends = "Inhabitant condition trends: \n";

            foreach (var inhabitant in inhabitants)
            {
                if (videoSensorDataMap.TryGetValue(inhabitant.Name, out var videoSensorData))
                {
                    if (double.TryParse(inhabitant.Condition, out double conditionValue))
                    {
                        var averageConditionValue = videoSensorData.Average(s => s.SensorValue);

                        string conditionStatus;

                        if (Math.Abs(averageConditionValue - conditionValue) < 10)
                        {
                            conditionStatus = "normal";
                            conditionIndicators.Add("normal");
                        }
                        else if (Math.Abs(averageConditionValue - conditionValue) > 10 &&
                            averageConditionValue < conditionValue)
                        {
                            conditionStatus = "worse";
                            conditionIndicators.Add("decreased");
                        }
                        else
                        {
                            conditionStatus = "abnormal";
                            conditionIndicators.Add("increased");
                        }

                        analysedTrends += $"{inhabitant.Name} condition is {conditionStatus} based on research analysis. \n";
                    }
                }
            }
            conditionIndicators.Add(analysedTrends);
            return conditionIndicators;
        }


        public enum AquariumProperty { Temperature, WaterLevel, Acidity, Lighting }

        private float GetAquariumProperty(AquariumProperty property, AquariumViewModel aquariumViewModel)
        {
            // Implement logic to retrieve property value based on property enum value
            switch (property)
            {
                case AquariumProperty.Temperature:
                    return (float)aquariumViewModel.Temperature;
                case AquariumProperty.WaterLevel:
                    return (float)aquariumViewModel.WaterLevel;
                case AquariumProperty.Acidity:
                    return (float)aquariumViewModel.Acidity;
                case AquariumProperty.Lighting:
                    return (float)aquariumViewModel.Lighting;
                default:
                    throw new ArgumentException($"Invalid property: {property}");
            }
        }

        public List<string> AnalyzeAquariumProperty(AquariumProperty property,
                                        Dictionary<string, List<SensorData>> sensorDataMap,
                                        AquariumViewModel aquariumViewModel)
        {
            var indicators = new List<string>();
            var analysedTrends = $"Aquarium {property} trends: \n";

            if (sensorDataMap.TryGetValue(aquariumViewModel.AquariumType, out var sensorData))
            {
                var averageValue = sensorData.Average(s => s.SensorValue);
                string conditionStatus;

                var propertyValue = GetAquariumProperty(property, aquariumViewModel);

                if (Math.Abs(averageValue - propertyValue) < 10)
                {
                    conditionStatus = "normal";
                    indicators.Add("normal");
                }
                else if (Math.Abs(averageValue - propertyValue) > 10 &&
                         averageValue < propertyValue)
                {
                    conditionStatus = "decreased";
                    indicators.Add("decreased");
                }
                else
                {
                    conditionStatus = "increased significantly";
                    indicators.Add("increased");
                }

                analysedTrends += $"{aquariumViewModel.AquariumType} {property.ToString()} is {conditionStatus} based on research analysis. \n";
            }

            indicators.Add(analysedTrends);
            return indicators;
        }

        public List<string> AnalyzeAquariumTemperature(Dictionary<string, List<SensorData>> temperatureSensorDataMap, AquariumViewModel aquariumViewModel)
        {
            return AnalyzeAquariumProperty(AquariumProperty.Temperature, temperatureSensorDataMap, aquariumViewModel);
        }

        public List<string> AnalyzeAquariumWaterLevel(Dictionary<string, List<SensorData>> waterLevelSensorDataMap, AquariumViewModel aquariumViewModel)
        {
            return AnalyzeAquariumProperty(AquariumProperty.WaterLevel, waterLevelSensorDataMap, aquariumViewModel);
        }

        public List<string> AnalyzeAquariumAcidity(Dictionary<string, List<SensorData>> aciditySensorDataMap, AquariumViewModel aquariumViewModel)
        {
            return AnalyzeAquariumProperty(AquariumProperty.Acidity, aciditySensorDataMap, aquariumViewModel);
        }

        public List<string> AnalyzeAquariumLighting(Dictionary<string, List<SensorData>> lightingSensorDataMap, AquariumViewModel aquariumViewModel)
        {
            return AnalyzeAquariumProperty(AquariumProperty.Lighting, lightingSensorDataMap, aquariumViewModel);
        }

    }

}
