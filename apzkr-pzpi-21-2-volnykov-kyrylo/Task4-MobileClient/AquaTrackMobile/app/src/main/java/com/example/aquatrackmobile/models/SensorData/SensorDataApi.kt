package com.example.aquatrackmobile.models.SensorData

import io.ktor.client.*
import io.ktor.client.call.*
import io.ktor.client.request.*
import io.ktor.client.statement.*
import io.ktor.http.*
import kotlinx.serialization.json.*

class SensorDataApiException(message: String, cause: Throwable? = null) : Exception(message, cause)

interface SensorDataApi {
    val client: HttpClient

    suspend fun getAllSensorData(): List<SensorData>
    suspend fun getSensorDataById(id: Int): SensorData
    suspend fun addSensorData(sensorData: SensorData): SensorData
    suspend fun deleteSensorData(id: Int)
}

class SensorDataApiImpl(override val client: HttpClient) : SensorDataApi {

    private val apiKey: String = "http://0.tcp.eu.ngrok.io:19926/api"

    override suspend fun getAllSensorData(): List<SensorData> {
        val response: HttpResponse = client.get("$apiKey/SensorData")
        if (response.status.isSuccess()) {
            println(response)
            return response.body()
        } else {
            throw SensorDataApiException("Failed to fetch all sensor data")
        }
    }

    override suspend fun getSensorDataById(id: Int): SensorData {
        val response: HttpResponse = client.get("$apiKey/SensorData/$id")
        if (response.status.isSuccess()) {
            return response.body()
        } else {
            throw SensorDataApiException("Failed to fetch sensor data by ID")
        }
    }

    override suspend fun addSensorData(sensorData: SensorData): SensorData {
        val response: HttpResponse = client.post("$apiKey/SensorData") {
            contentType(ContentType.Application.Json)
            setBody(sensorData)

        }
        println(sensorData)
        println(response)
        if (response.status.isSuccess()) {
            return response.body()
        } else {
            throw SensorDataApiException("Failed to add sensor data")
        }
    }

    override suspend fun deleteSensorData(id: Int) {
        val response: HttpResponse = client.delete("$apiKey/SensorData/$id")
        if (!response.status.isSuccess()) {
            throw SensorDataApiException("Failed to delete sensor data")
        }
    }

}