package com.example.aquatrackmobile.viewmodels

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.aquatrackmobile.models.SensorData.SensorDataApi
import com.example.aquatrackmobile.models.SensorData.SensorData
import kotlinx.coroutines.launch
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow

class SensorDataViewModel(private val sensorDataApi: SensorDataApi) : ViewModel() {
    private val _sensorDataList = MutableStateFlow<List<SensorData>>(emptyList())
    val sensorDataList: StateFlow<List<SensorData>> = _sensorDataList

    private val _sensorData = MutableStateFlow<SensorData?>(null)
    val sensorData: StateFlow<SensorData?> = _sensorData

    init {
        fetchAllSensorData()
    }

    fun fetchAllSensorData() {
        viewModelScope.launch {
            try {
                _sensorDataList.value = sensorDataApi.getAllSensorData()
                println("Length of sensorDataList: ${sensorDataList.value.size}")
                println("SensorDataList: ${sensorDataList.value}")
            } catch (e: Exception) {
                println("Error fetching sensor data: ${e.message}")
            }
        }
    }

    fun fetchSensorDataById(id: Int) {
        viewModelScope.launch {
            try {
                _sensorData.value = sensorDataApi.getSensorDataById(id)
            } catch (e: Exception) {
                // Handle error
            }
        }
    }

    fun addSensorData(sensorData: SensorData) {
        viewModelScope.launch {
            try {
                _sensorData.value = sensorDataApi.addSensorData(sensorData)
            } catch (e: Exception) {
                println("Error fetching sensor data: ${e.message}")
            }
        }
    }

    fun deleteSensorData(id: Int) {
        viewModelScope.launch {
            try {
                sensorDataApi.deleteSensorData(id)
                fetchAllSensorData() // Refresh the list after deletion
            } catch (e: Exception) {
                // Handle error
            }
        }
    }
}
