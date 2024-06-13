package com.example.aquatrackmobile.pages.FeedingSchedule

import androidx.compose.foundation.layout.*
import androidx.compose.material3.*
import androidx.compose.runtime.*
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import com.example.aquatrackmobile.models.FeedingSchedule.FeedingSchedule
import com.example.aquatrackmobile.viewmodels.FeedingScheduleViewModel

@Composable
fun AddFeedingSchedulePage(
    viewModel: FeedingScheduleViewModel,
    aquariumId: Int,
    onAddComplete: () -> Unit
) {
    var feedType by remember { mutableStateOf("") }
    var feedAmount by remember { mutableStateOf("") }
    var feedTime by remember { mutableStateOf("") }
    var repeatInterval by remember { mutableStateOf("") }
    var isActive by remember { mutableStateOf(true) }

    Column(modifier = Modifier
        .fillMaxSize()
        .padding(16.dp)) {
        TextField(
            value = feedType,
            onValueChange = { feedType = it },
            label = { Text("Feed Type") }
        )
        TextField(
            value = feedAmount,
            onValueChange = { feedAmount = it },
            label = { Text("Feed Amount") }
        )
        TextField(
            value = feedTime,
            onValueChange = { feedTime = it },
            label = { Text("Feed Time") }
        )
        TextField(
            value = repeatInterval,
            onValueChange = { repeatInterval = it },
            label = { Text("Repeat Interval") }
        )
        Switch(
            checked = isActive,
            onCheckedChange = { isActive = it },
            modifier = Modifier.padding(top = 8.dp)
        )

        Button(
            onClick = {
                viewModel.addFeedingScheduleForCurrentUser(
                    FeedingSchedule(
                        feedingScheduleId = 0,
                        aquariumId = aquariumId,
                        feedType = feedType,
                        feedAmount = feedAmount,
                        feedTime = feedTime,
                        repeatInterval = repeatInterval,
                        active = isActive
                    )
                )
                onAddComplete()
            },
            modifier = Modifier.padding(top = 16.dp)
        ) {
            Text("Add Feeding Schedule")
        }
    }
}
