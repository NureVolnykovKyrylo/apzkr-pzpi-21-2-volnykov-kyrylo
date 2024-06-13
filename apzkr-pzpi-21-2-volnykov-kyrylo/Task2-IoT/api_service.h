#ifndef API_COMMUNICATION_H
#define API_COMMUNICATION_H

#include <WiFi.h>
#include <ArduinoJson.h>
#include <HTTPClient.h>
#include "constants.h"
#include "sensor_service.h"
#include <vector>
#include "feeding_schedule_service.h"

bool authenticateUser(const String& userEmail, const String& userPassword) {
  if (WiFi.status() == WL_CONNECTED) {
    HTTPClient http;

    http.begin(String(API_BASE_URL) + "/api/User/login");
    http.addHeader("Content-Type", "application/json");

    String requestBody = "{\"email\":\"" + userEmail + "\",\"password\":\"" + userPassword + "\"}";
    int httpResponseCode = http.POST(requestBody);
    String responseBody = http.getString();

    if (httpResponseCode > 0) {
      if (httpResponseCode == HTTP_CODE_OK) {
        String response = http.getString();
        Serial.println("Authentication successful");
        return true;
      } else {
        Serial.println("Authentication failed");
        return false;
      }
    } else {
      Serial.println("Error on authentication");
      return false;
    }
    http.end();
  }
}

int getSensorByIdentificator(const String& sensorIdentificator, int sensorType) {
  if (WiFi.status() == WL_CONNECTED) {
    HTTPClient http;
    String url = String(API_BASE_URL) + "/api/SensorData/find";
    url += "?sensorIdentificator=" + sensorIdentificator;
    url += "&sensorType=" + String(sensorType);

    http.begin(url);
    http.addHeader("Content-Type", "application/json");

    int httpResponseCode = http.GET();

    if (httpResponseCode > 0) {
      if (httpResponseCode == HTTP_CODE_OK) {
        String response = http.getString();
        Serial.println("Sensor data retrieved successfully");

        DynamicJsonDocument jsonResponse(1024);
        DeserializationError error = deserializeJson(jsonResponse, response);

        if (error) {
          Serial.println("Failed to parse JSON");
          return -1;
        }

        int sensorDataId = jsonResponse["sensorDataId"];
        Serial.print("Retrieved Sensor Data ID: ");
        Serial.println(sensorDataId);
        return sensorDataId;
      } else {
        Serial.println("Failed to get sensor data");
        return -1;
      }
    } else {
      Serial.println("Error on GET request");
      return -1;
    }
    http.end();
  }
}

DynamicJsonDocument getAquariumByType(String aquariumType) {
  DynamicJsonDocument jsonResponse(1024);

  if (WiFi.status() == WL_CONNECTED) {
    HTTPClient http;
    String url = String(API_BASE_URL) + "/api/Aquarium/find";
    url += "?aquariumType=" + String(aquariumType);

    http.begin(url);
    http.addHeader("Content-Type", "application/json");

    int httpResponseCode = http.GET();

    if (httpResponseCode > 0) {
      if (httpResponseCode == HTTP_CODE_OK) {
        String response = http.getString();
        Serial.println("Aquarium data retrieved successfully");

        DeserializationError error = deserializeJson(jsonResponse, response);

        if (error) {
          Serial.println("Failed to parse JSON");
          jsonResponse.clear();
        } else {
          //Serial.println("Aquarium data parsed successfully");
        }
      } else {
        Serial.println("Failed to get aquarium data");
        jsonResponse.clear();
      }
    } else {
      Serial.println("Error on GET request");
      jsonResponse.clear();
    }
    http.end();
  } else {
    Serial.println("WiFi not connected");
    jsonResponse.clear();
  }

  return jsonResponse;
}

bool updateSensorData(const SensorData& sensor) {
  if (WiFi.status() == WL_CONNECTED) {
    HTTPClient http;
    String url = String(API_BASE_URL) + "/api/SensorData/" + String(sensor.sensorDataId);
    http.begin(url);
    http.addHeader("Content-Type", "application/json");

    DynamicJsonDocument jsonBody(1024);
    jsonBody["sensorDataId"] = sensor.sensorDataId;
    jsonBody["sensorValue"] = sensor.sensorValue;
    jsonBody["timestamp"] = sensor.timestamp.c_str();
    jsonBody["sensorStatus"] = sensor.sensorStatus.c_str();

    String requestBody;
    serializeJson(jsonBody, requestBody);

    int httpResponseCode = http.PUT(requestBody);

    Serial.println("Request body: " + requestBody);
    
    if (httpResponseCode > 0) {
      if (httpResponseCode == HTTP_CODE_OK) {
        Serial.println("Sensor data updated successfully");
        return true;
      } else {
        Serial.println("Failed to update sensor data. Response code: " + String(httpResponseCode));
        return false;
      }
    } else {
      Serial.println("Error on HTTP request");
      return false;
    }
    http.end();
  }
}

std::vector<FeedingSchedule> getFeedingSchedulesForAquairum(int aquariumId) {
  std::vector<FeedingSchedule> feedingSchedules;

  if (WiFi.status() == WL_CONNECTED) {
    HTTPClient http;
    String url = String(API_BASE_URL) + "/api/FeedingSchedule/aquarium/";
    url += String(aquariumId);
    //Serial.println(String(aquariumId));
    
    http.begin(url);
    http.addHeader("Content-Type", "application/json");

    int httpResponseCode = http.GET();

    if (httpResponseCode > 0) {
      if (httpResponseCode == HTTP_CODE_OK) {
        String response = http.getString();
        Serial.println("Feeding schedules retrieved successfully");
        //Serial.print("Response body: ");
        //Serial.println(response);

        DynamicJsonDocument jsonResponse(2048);
        DeserializationError error = deserializeJson(jsonResponse, response);

        if (error) {
          Serial.println("Failed to parse JSON");
        } else {
          JsonArray schedulesArray = jsonResponse.as<JsonArray>();
          for (JsonObject schedule : schedulesArray) {
            FeedingSchedule feedingSchedule;
            feedingSchedule.feedingScheduleId = schedule["feedingScheduleId"];
            feedingSchedule.aquariumId = schedule["aquariumId"];
            feedingSchedule.feedTime = schedule["feedTime"];
            feedingSchedule.feedAmount = schedule["feedAmount"] | 0.0;
            feedingSchedule.feedType = schedule["feedType"].as<String>();
            feedingSchedule.repeatInterval = schedule["repeatInterval"] | 0.0;
            feedingSchedule.active = schedule["active"];
            feedingSchedules.push_back(feedingSchedule);
          }
          Serial.println("Feeding schedules parsed successfully");
        }
      } else {
        Serial.print("Failed to get feeding schedules, HTTP response code: ");
        Serial.println(httpResponseCode);
        String response = http.getString();
        Serial.print("Response body: ");
        Serial.println(response);
      }
    } else {
      Serial.println("Error on GET request");
    }
    http.end();
  } else {
    Serial.println("WiFi not connected");
  }

  return feedingSchedules;
}

#endif // API_COMMUNICATION_H
