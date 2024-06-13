package com.example.aquatrackmobile.pages.SensorData

import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Add
import androidx.compose.material.icons.filled.ArrowBack
import androidx.compose.material3.*
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import com.example.aquatrackmobile.models.SensorData.SensorData
import com.example.aquatrackmobile.viewmodels.SensorDataViewModel

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun SensorDataPage(
    viewModel: SensorDataViewModel,
    onBackClick: () -> Unit,
    onAddSensorData: () -> Unit,
    onUpdateSensorData: (SensorData) -> Unit,
    onDeleteSensorData: (Int) -> Unit
) {
    val sensorDataList by viewModel.sensorDataList.collectAsState()

    LaunchedEffect(Unit) {
        viewModel.fetchAllSensorData()
    }

    Scaffold(
        topBar = {
            TopAppBar(
                title = { Text("Sensor Data") },
                navigationIcon = {
                    IconButton(onClick = onBackClick) {
                        Icon(Icons.Default.ArrowBack, contentDescription = "Back")
                    }
                },
                actions = {
                    IconButton(onClick = onAddSensorData) {
                        Icon(Icons.Default.Add, contentDescription = "Add Sensor Data")
                    }
                }
            )
        }
    ) { paddingValues ->
        Box(
            modifier = Modifier
                .fillMaxSize()
                .padding(paddingValues)
        ) {
            if (sensorDataList.isEmpty()) {
                Text(
                    text = "No sensor data found.",
                    style = MaterialTheme.typography.bodyLarge,
                    modifier = Modifier
                        .padding(16.dp)
                        .align(Alignment.Center)
                )
            } else {
                LazyColumn(
                    modifier = Modifier.fillMaxSize(),
                    contentPadding = PaddingValues(vertical = 8.dp)
                ) {
                    items(sensorDataList) { data ->
                        SensorDataCard(
                            sensorData = data,
                            onUpdate = { onUpdateSensorData(data) },
                            onDelete = { onDeleteSensorData(data.sensorDataId) }
                        )
                    }
                }
            }
        }
    }
}
