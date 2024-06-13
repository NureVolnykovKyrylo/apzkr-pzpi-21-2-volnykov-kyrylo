package com.example.aquatrackmobile.pages.SensorData

import androidx.compose.foundation.layout.*
import androidx.compose.material3.*
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.unit.dp
import com.example.aquatrackmobile.models.SensorData.SensorData

@Composable
fun SensorDataCard(
    sensorData: SensorData,
    onUpdate: () -> Unit,
    onDelete: () -> Unit
) {
    Card(
        modifier = Modifier
            .fillMaxWidth()
            .padding(8.dp)
    ) {
        Column(modifier = Modifier.padding(16.dp)) {
            Text(
                text = "Sensor Data ID: ${sensorData.sensorDataId}",
                fontWeight = FontWeight.Bold,
                style = MaterialTheme.typography.bodyLarge
            )
            Text(text = "Sensor Type: ${sensorData.sensorType}", style = MaterialTheme.typography.bodyMedium)
            Text(text = "Value: ${sensorData.sensorValue}", style = MaterialTheme.typography.bodyMedium)
            Text(text = "Timestamp: ${sensorData.timestamp}", style = MaterialTheme.typography.bodyMedium)
            Text(text = "Sensor Identificator: ${sensorData.sensorIdentificator}", style = MaterialTheme.typography.bodyMedium)

            Spacer(modifier = Modifier.height(8.dp))

            Row {
                Spacer(modifier = Modifier.width(8.dp))
                Button(onClick = onDelete) {
                    Text("Delete")
                }
            }
        }
    }
}
