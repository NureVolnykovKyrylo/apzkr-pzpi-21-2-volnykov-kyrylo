package com.example.aquatrackmobile.pages.SensorData

import androidx.compose.foundation.layout.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.ArrowBack
import androidx.compose.material3.*
import androidx.compose.runtime.*
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import com.example.aquatrackmobile.models.SensorData.SensorData
import com.example.aquatrackmobile.models.SensorData.SensorType
import com.example.aquatrackmobile.viewmodels.SensorDataViewModel

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun AddSensorDataPage(
    viewModel: SensorDataViewModel,
    onAddComplete: () -> Unit,
    onBackClick: () -> Unit
) {
    var sensorType by remember { mutableStateOf(SensorType.TEMPERATURE) }
    var sensorValue by remember { mutableStateOf("") }
    var timestamp by remember { mutableStateOf("") }
    var sensorStatus by remember { mutableStateOf("") }
    var sensorIdentifier by remember { mutableStateOf("") }
    var expanded by remember { mutableStateOf(false) }

    Scaffold(
        topBar = {
            TopAppBar(
                title = { Text("Add Sensor Data") },
                navigationIcon = {
                    IconButton(onClick = onBackClick) {
                        Icon(Icons.Default.ArrowBack, contentDescription = "Back")
                    }
                }
            )
        }
    ) { innerPadding ->
        Column(
            modifier = Modifier
                .fillMaxSize()
                .padding(innerPadding)
                .padding(16.dp),
            verticalArrangement = Arrangement.spacedBy(16.dp)
        ) {
            ExposedDropdownMenuBox(
                expanded = expanded,
                onExpandedChange = { expanded = !expanded }
            ) {
                TextField(
                    value = sensorType.name,
                    onValueChange = {},
                    readOnly = true,
                    label = { Text("Sensor Type") },
                    trailingIcon = { ExposedDropdownMenuDefaults.TrailingIcon(expanded = expanded) },
                    colors = ExposedDropdownMenuDefaults.textFieldColors(),
                    modifier = Modifier.fillMaxWidth().menuAnchor()
                )

                ExposedDropdownMenu(
                    expanded = expanded,
                    onDismissRequest = { expanded = false },
                    modifier = Modifier.fillMaxWidth()
                ) {
                    SensorType.values().forEach { type ->
                        DropdownMenuItem(
                            text = { Text(type.name) },
                            onClick = {
                                sensorType = type
                                expanded = false
                            }
                        )
                    }
                }
            }

            OutlinedTextField(
                value = sensorValue,
                onValueChange = { sensorValue = it },
                label = { Text("Sensor Value") },
                modifier = Modifier.fillMaxWidth()
            )
            OutlinedTextField(
                value = timestamp,
                onValueChange = { timestamp = it },
                label = { Text("Timestamp") },
                modifier = Modifier.fillMaxWidth()
            )
            OutlinedTextField(
                value = sensorStatus,
                onValueChange = { sensorStatus = it },
                label = { Text("Sensor Status") },
                modifier = Modifier.fillMaxWidth()
            )
            OutlinedTextField(
                value = sensorIdentifier,
                onValueChange = { sensorIdentifier = it },
                label = { Text("Sensor Identifier") },
                modifier = Modifier.fillMaxWidth()
            )

            Button(
                onClick = {
                    viewModel.addSensorData(
                        SensorData(
                            sensorDataId = 0, // Assuming this will be generated on the server side
                            sensorType = sensorType.id ,
                            sensorValue = sensorValue,
                            timestamp = "2024-06-05T21:59:37.379Z",
                            sensorStatus = sensorStatus,
                            sensorIdentificator = sensorIdentifier
                        )
                    )
                    onAddComplete()
                },
                modifier = Modifier.fillMaxWidth()
            ) {
                Text("Add Sensor Data")
            }
        }
    }
}