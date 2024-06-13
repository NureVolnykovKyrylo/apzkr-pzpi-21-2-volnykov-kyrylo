package com.example.aquatrackmobile.pages.FeedingSchedule

import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.ArrowBack
import androidx.compose.material.icons.filled.Add
import androidx.compose.material3.*
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import com.example.aquatrackmobile.models.FeedingSchedule.FeedingSchedule
import com.example.aquatrackmobile.pages.FeedingSchedule.FeedingScheduleCard
import com.example.aquatrackmobile.viewmodels.FeedingScheduleViewModel

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun FeedingSchedulePage(
    aquariumId: Int,
    viewModel: FeedingScheduleViewModel,
    onBackClick: () -> Unit,
    onAddSchedule: () -> Unit,
    onUpdateSchedule: (FeedingSchedule) -> Unit
) {
    val feedingSchedules by viewModel.feedingScheduleList.collectAsState()

    LaunchedEffect(aquariumId) {
        viewModel.fetchFeedingSchedulesByAquariumId(aquariumId)
    }

    Scaffold(
        topBar = {
            TopAppBar(
                title = { Text("Feeding Schedules") },
                navigationIcon = {
                    IconButton(onClick = onBackClick) {
                        Icon(Icons.Default.ArrowBack, contentDescription = "Back")
                    }
                }
            )
        },
        floatingActionButton = {
            FloatingActionButton(onClick = onAddSchedule) {
                Icon(Icons.Default.Add, contentDescription = "Add Schedule")
            }
        }
    ) { paddingValues ->
        Box(
            modifier = Modifier
                .fillMaxSize()
                .padding(paddingValues)
        ) {
            if (feedingSchedules.isEmpty()) {
                Text(
                    text = "No feeding schedules found.",
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
                    items(feedingSchedules) { schedule ->
                        FeedingScheduleCard(
                            feedingSchedule = schedule,
                            onDelete = { schedule -> viewModel.deleteFeedingScheduleForCurrentUser(schedule.feedingScheduleId, aquariumId) },
                            onUpdate = onUpdateSchedule
                        )
                    }
                }
            }
        }
    }
}
