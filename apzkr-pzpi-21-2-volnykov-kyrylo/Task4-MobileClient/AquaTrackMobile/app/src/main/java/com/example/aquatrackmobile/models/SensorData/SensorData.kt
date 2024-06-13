package com.example.aquatrackmobile.models.SensorData

import com.example.aquatrackmobile.models.Aquarium.Aquarium
import kotlinx.serialization.Serializable
import kotlinx.serialization.SerialName

@Serializable
data class SensorDataResponse(
    val sensorData: SensorData
)

@Serializable
data class SensorData(
    val sensorDataId: Int,
    val sensorType: Int,
    val sensorValue: String,
    val timestamp: String,
    val sensorStatus: String,
    val sensorIdentificator: String
)

enum class SensorType(val id: Int) {
    @SerialName("0") TEMPERATURE(0),
    @SerialName("1") WATERLEVEL(1),
    @SerialName("2") LIGHTNING(2),
    @SerialName("3") ACIDITY(3),
    @SerialName("4") MOVEMENT(4),
    @SerialName("5") VIDEO(5);
}