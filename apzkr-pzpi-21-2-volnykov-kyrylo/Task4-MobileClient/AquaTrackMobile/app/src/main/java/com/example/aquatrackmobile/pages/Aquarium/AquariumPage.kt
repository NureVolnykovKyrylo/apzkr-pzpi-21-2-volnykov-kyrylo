package com.example.aquatrackmobile.pages.Aquarium

import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.AccountCircle
import androidx.compose.material.icons.filled.Info
import androidx.compose.material3.*
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import com.example.aquatrackmobile.viewmodels.AquariumViewModel
import com.example.aquatrackmobile.viewmodels.UserViewModel
import kotlinx.coroutines.flow.collectLatest

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun AquariumPage(
    userViewModel: UserViewModel,
    aquariumViewModel: AquariumViewModel,
    onLogout: () -> Unit,
    onViewFeedingSchedules: (Int) -> Unit, // Takes aquarium ID as parameter
    onViewSensorData: () -> Unit // Navigate to Sensor Data Page
) {
    val user by userViewModel.user.collectAsState()
    val aquariums by aquariumViewModel.userAquariums.collectAsState()
    val isLoading by aquariumViewModel.isLoading.collectAsState()

    LaunchedEffect(user) {
        user?.let { currentUser ->
            aquariumViewModel.fetchAquariumsForUser(currentUser.userId)
        }
    }

    LaunchedEffect(aquariumViewModel) {
        aquariumViewModel.error.collectLatest { errorMsg ->
            // Show error in a snackbar or dialog
        }
    }

    Scaffold(
        topBar = {
            TopAppBar(
                title = { Text("My Aquariums") },
                actions = {
                    IconButton(onClick = {
                        userViewModel.logoutUser()
                        onLogout()
                    }) {
                        Icon(Icons.Default.AccountCircle, contentDescription = "Logout")
                    }
                }
            )
        },
        floatingActionButton = {
            FloatingActionButton(
                onClick = { onViewSensorData() },
                modifier = Modifier
                    .padding(16.dp)
            ) {
                Icon(Icons.Default.Info, contentDescription = "Sensor Data")
            }
        }
    ) { paddingValues ->
        Box(
            modifier = Modifier
                .fillMaxSize()
                .padding(paddingValues)
        ) {
            if (isLoading) {
                CircularProgressIndicator(modifier = Modifier.align(Alignment.Center))
            } else if (aquariums.isEmpty()) {
                Text(
                    text = "No aquariums yet. Add one to get started!",
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
                    item {
                        Text(
                            text = "Welcome back, ${user?.firstName}!",
                            style = MaterialTheme.typography.headlineMedium,
                            modifier = Modifier.padding(16.dp)
                        )
                    }
                    items(aquariums) { aquarium ->
                        AquariumCard(
                            aquarium = aquarium,
                            onViewSchedules = {
                                onViewFeedingSchedules(aquarium.aquariumId) // Navigate to Feeding Schedules
                            }
                        )
                    }
                }
            }
        }
    }
}

