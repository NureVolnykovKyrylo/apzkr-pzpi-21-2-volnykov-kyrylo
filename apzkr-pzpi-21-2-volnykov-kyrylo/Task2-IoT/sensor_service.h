#ifndef SENSOR_FUNCTIONS_H
#define SENSOR_FUNCTIONS_H

#include <DHT.h>
#include <TimeLib.h>
#include <NTPClient.h>
#include <WiFiUdp.h>
#include "constants.h"

/* Init arduino's humidity/temperature sensor */
DHT dht(DHT_PIN, DHT_TYPE);

WiFiUDP ntpUDP;
NTPClient timeClient(ntpUDP, "pool.ntp.org");



enum SensorType {
  TemperatureSensor = 0,
  WaterLevelSensor,
  LightningSensor,
  AciditySensor,
  MovementSensor,
  VideoSensor
};

struct SensorData {
  int sensorDataId;
  SensorType sensorType;
  double sensorValue;
  String timestamp;
  String sensorStatus;
  String sensorIdentificator;
};

typedef struct {
    float T_normal;    // Normal temperature
    float L_normal;    // Normal water level
    float T_current;   // Current temperature
    float H_normal;    // Normal humidity
    float H_current;   // Current humidity
} AquariumParams;

/* k - Empirical constant, determined through observation */
float calculateCurrentWaterLevel(AquariumParams params, float k) {

    // Calculate the change in evaporation rate
    float deltaE = k * ((params.T_current - params.T_normal) / params.T_normal - 
                        (params.H_current - params.H_normal) / params.H_normal);

    // Calculate the water loss
    float deltaL = deltaE;

    // Calculate the current water level
    float L_current = params.L_normal - deltaL;

    return L_current;
}

float readHumidity() {
  return dht.readHumidity();
}

float readTemperature() {
  return dht.readTemperature();
}

String getCurrentTimestamp() {
  timeClient.update();
  time_t now = timeClient.getEpochTime();

  char timestamp[25];
  sprintf(timestamp, "%04d-%02d-%02dT%02d:%02d:%02dZ",
          year(now), month(now), day(now),
          hour(now), minute(now), second(now));
  return String(timestamp);
}

#endif // SENSOR_FUNCTIONS_H
