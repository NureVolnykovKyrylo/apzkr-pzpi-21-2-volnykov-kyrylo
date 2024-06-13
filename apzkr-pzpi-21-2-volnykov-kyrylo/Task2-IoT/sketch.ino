#include <Arduino.h>
#include <DHT.h>
#include <WiFi.h>
#include <HTTPClient.h>

#include "constants.h"
#include "api_service.h"
#include "sensor_service.h"
#include <vector>

void setup() {
  Serial.begin(9600);
  WiFi.begin(SSID, WIFI_PASSWORD);
  dht.begin();
  timeClient.begin();
  timeClient.setTimeOffset(0);
}

void loop() {
  int videoPotValue = analogRead(VIDEO_POTENTIOMETER_PIN);
  int movementPotValue = analogRead(MOVEMENT_POTENTIOMETER_PIN);
  int acidityPotValue = analogRead(ACIDITY_POTENTIOMETER_PIN);

  /* Map the potentiometer's values to the range of 0 to 100 */
  int animalConditionValue = map(videoPotValue, 0, 4095, 0, 100);
  int animalActivityValue = map(movementPotValue, 0, 4095, 0, 100);
  int acidityValue = map(acidityPotValue, 0, 4095, 0, 100);

  double humidityValue = readHumidity();
  double temperatureValue = readTemperature();

  int lightningValue = analogRead(LIGHT_SENSOR_PIN);

  Serial.println("animal condition value: " + String(animalConditionValue));
  Serial.println("animal activity value: " + String(animalActivityValue));
  Serial.println("acidity: " + String(acidityValue));
  Serial.println("humidity: " + String(humidityValue));
  Serial.println("temperature: " + String(temperatureValue));
  Serial.println("lightning: " + String(lightningValue));

  delay(1000);
  Serial.println("current time " + getCurrentTimestamp());

 

  /* Init sensor datas */
  SensorData waterLevelSensor = {0, WaterLevelSensor, 0.0,
                                 getCurrentTimestamp(), "active", "SharkAquarium"
                                };

  SensorData temperatureSensor = {0, TemperatureSensor, temperatureValue,
                                  getCurrentTimestamp(), "active", "SharkAquarium"
                                 };

  SensorData aciditySensor = {0, AciditySensor, acidityValue,
                              getCurrentTimestamp(), "active", "SharkAquarium"
                             };

  SensorData lightningSensor = {0, LightningSensor, lightningValue,
                                getCurrentTimestamp(), "active", "SharkAquarium"
                               };

  SensorData movementSensor = {0, MovementSensor, animalActivityValue,
                               getCurrentTimestamp(), "active", "WhiteShark"
                              };

  SensorData videoSensor = {0, VideoSensor, animalConditionValue,
                            getCurrentTimestamp(), "active", "WhiteShark"
                           };

  /* Update sensor data on server */
  if (WiFi.status() == WL_CONNECTED) {
    String userEmail = "kvolnykov@gmail.com";
    String userPassword = "user123";
    bool authSuccess = authenticateUser(userEmail, userPassword);

    if (authSuccess) {
      Serial.println("User logged in successfully");

      int waterLevelSensorId =
        getSensorByIdentificator(waterLevelSensor.sensorIdentificator, waterLevelSensor.sensorType);

      int temperatureSensorId
        = getSensorByIdentificator(temperatureSensor.sensorIdentificator, temperatureSensor.sensorType);

      int aciditySensorId
        = getSensorByIdentificator(aciditySensor.sensorIdentificator, aciditySensor.sensorType);

      int lightningSensorId
        = getSensorByIdentificator(lightningSensor.sensorIdentificator, lightningSensor.sensorType);
        
      int videoSensorId
        = getSensorByIdentificator(videoSensor.sensorIdentificator, videoSensor.sensorType);

      int movementSensorId
        = getSensorByIdentificator(movementSensor.sensorIdentificator, movementSensor.sensorType);

      /* Retrieve aquarium data */
      DynamicJsonDocument aquariumData = getAquariumByType("SharkAquarium");

      if (!aquariumData.isNull()) {
        float normalTemperature = aquariumData["temperature"];
        int normalWaterLevel = aquariumData["waterLevel"];
        int aquariumId = aquariumData["aquariumId"];

        std::vector<FeedingSchedule> feedingSchedules = getFeedingSchedulesForAquairum(aquariumId);
        //Serial.println(String(feedingSchedules.size()));
        for (const auto& schedule : feedingSchedules) {
          if (schedule.active) {
            Serial.println("Schedule Activated: ");
            Serial.print("Feeding Schedule ID: ");
            Serial.println(schedule.feedingScheduleId);
            Serial.print("Aquarium ID: ");
            Serial.println(schedule.aquariumId);
            Serial.print("Feed Time: ");
            Serial.println(schedule.feedTime);
            Serial.print("Feed Amount: ");
            Serial.println(schedule.feedAmount);
            Serial.print("Feed Type: ");
            Serial.println(schedule.feedType);
            Serial.print("Repeat Interval: ");
            Serial.println(schedule.repeatInterval);
            Serial.println("---------");
            break;
          }
        }

        /* Calc current water level */
        AquariumParams params = {normalTemperature, normalWaterLevel, temperatureSensor.sensorValue, 50.0, readHumidity()};
        float currentWaterLevel = calculateCurrentWaterLevel(params, 1.0);
        waterLevelSensor.sensorValue = currentWaterLevel;
        Serial.println("Current Water Level: " + String(currentWaterLevel));

      } else {
        Serial.println("Failed to retrieve aquarium data");
      }

      /* Update water level sensor data on server */
      if (waterLevelSensorId != -1) {
        waterLevelSensor.sensorDataId = waterLevelSensorId;

        if (!updateSensorData(waterLevelSensor)) {
          Serial.println("Failed to update water level sensor data");
        }

      } else {
        Serial.println("Failed to get water level sensor data");
      }

      /* Update temperature sensor data on server */
      if (temperatureSensorId != -1) {
        temperatureSensor.sensorDataId = temperatureSensorId;

        if (!updateSensorData(temperatureSensor)) {
          Serial.println("Failed to update temperature sensor data");
        } 

      } else {
        Serial.println("Failed to get temperature sensor data");
      }

      /* Update acidity sensor data on server */
      if (aciditySensorId != -1) {
        aciditySensor.sensorDataId = aciditySensorId;

        if (!updateSensorData(aciditySensor)) {
          Serial.println("Failed to update acidity sensor data");
        } 

      } else {
        Serial.println("Failed to get acidity sensor data");
      }

      /* Update lightning sensor data on server */
      if (lightningSensorId != -1) {
        lightningSensor.sensorDataId = lightningSensorId;

        if (!updateSensorData(lightningSensor)) {
          Serial.println("Failed to update lightning sensor data");
        } 
      } else {
        Serial.println("Failed to get lightning sensor data");
      }

      /* Update video sensor data on server */
      if (videoSensorId != -1) {
        videoSensor.sensorDataId = videoSensorId;

        if (!updateSensorData(videoSensor)) {
          Serial.println("Failed to update video sensor data");
        }

      } else {
        Serial.println("Failed to get video sensor data");
      }

      /* Update movement sensor data on server */
      if (movementSensorId != -1) {
        movementSensor.sensorDataId = movementSensorId;

        if (!updateSensorData(movementSensor)) {
          Serial.println("Failed to update movement sensor data");
        }
      } else {
        Serial.println("Failed to get movement sensor data");
      }
    }

    delay(FREQUENCY);
  }
}

