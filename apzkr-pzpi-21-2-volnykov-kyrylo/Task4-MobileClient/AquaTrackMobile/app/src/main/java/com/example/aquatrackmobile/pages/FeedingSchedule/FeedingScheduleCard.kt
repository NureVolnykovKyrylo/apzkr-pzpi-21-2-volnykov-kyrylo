package com.example.aquatrackmobile.pages.FeedingSchedule

import androidx.compose.foundation.layout.*
import androidx.compose.material3.*
import androidx.compose.runtime.*
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.unit.dp
import com.example.aquatrackmobile.models.FeedingSchedule.FeedingSchedule

@Composable
fun FeedingScheduleCard(
    feedingSchedule: FeedingSchedule,
    onUpdate: (FeedingSchedule) -> Unit,
    onDelete: (FeedingSchedule) -> Unit
) {
    Card(
        modifier = Modifier
            .fillMaxWidth()
            .padding(8.dp)
    ) {
        Column(modifier = Modifier.padding(16.dp)) {
            Text(
                text = "Schedule ID: ${feedingSchedule.feedingScheduleId}",
                fontWeight = FontWeight.Bold,
                style = MaterialTheme.typography.bodyLarge
            )
            Text(text = "Aquarium ID: ${feedingSchedule.aquariumId}", style = MaterialTheme.typography.bodyMedium)
            Text(text = "Feed type: ${feedingSchedule.feedType}", style = MaterialTheme.typography.bodyMedium)
            Text(text = "Feed amount: ${feedingSchedule.feedAmount}", style = MaterialTheme.typography.bodyMedium)
            Text(text = "Time of the day to feed: ${feedingSchedule.feedTime}", style = MaterialTheme.typography.bodyMedium)
            Text(text = "Repeat interval: ${feedingSchedule.repeatInterval}", style = MaterialTheme.typography.bodyMedium)
            Text(text = "Is active: ${feedingSchedule.active}", style = MaterialTheme.typography.bodyMedium)

            Row(modifier = Modifier.fillMaxWidth(), horizontalArrangement = Arrangement.SpaceBetween) {
                Button(onClick = { onUpdate(feedingSchedule) }) {
                    Text("Update")
                }
                Button(onClick = { onDelete(feedingSchedule) }) {
                    Text("Delete")
                }
            }
        }
    }
}
