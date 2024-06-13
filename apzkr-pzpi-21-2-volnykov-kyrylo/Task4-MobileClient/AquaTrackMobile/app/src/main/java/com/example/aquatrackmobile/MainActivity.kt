package com.example.aquatrackmobile

import android.content.Context
import android.content.SharedPreferences
import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.activity.enableEdgeToEdge
import androidx.compose.foundation.layout.*
import androidx.compose.material3.*
import androidx.compose.runtime.*
import androidx.compose.ui.*
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import androidx.navigation.compose.*
import com.example.aquatrackmobile.models.Aquarium.AquariumApiImpl
import com.example.aquatrackmobile.models.User.UserApiImpl
import com.example.aquatrackmobile.models.FeedingSchedule.FeedingScheduleApiImpl
import com.example.aquatrackmobile.models.SensorData.SensorDataApiImpl

import com.example.aquatrackmobile.network.HttpClientProvider.client
import com.example.aquatrackmobile.pages.FeedingSchedule.AddFeedingSchedulePage
import com.example.aquatrackmobile.pages.FeedingSchedule.UpdateFeedingSchedulePage

import com.example.aquatrackmobile.pages.LoginPage
import com.example.aquatrackmobile.pages.SignUpPage
import com.example.aquatrackmobile.pages.Aquarium.AquariumPage
import com.example.aquatrackmobile.pages.FeedingSchedule.FeedingSchedulePage
import com.example.aquatrackmobile.pages.SensorData.SensorDataPage
import com.example.aquatrackmobile.pages.SensorData.AddSensorDataPage


import com.example.aquatrackmobile.viewmodels.*

class MainActivity : ComponentActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        val prefs = getSharedPreferences("aquatrack", Context.MODE_PRIVATE)
        enableEdgeToEdge()
        setContent {
            Scaffold(modifier = Modifier.fillMaxSize()) { innerPadding ->
                Column(modifier = Modifier.padding(innerPadding)) {
                    App(prefs)
                }
            }
        }
    }
}

@Composable
fun App(prefs: SharedPreferences) {
    val userApi = UserApiImpl(client)
    val userViewModel = UserViewModel(userApi)

    val aquariumApi = AquariumApiImpl(client)
    val aquariumViewModel = AquariumViewModel(aquariumApi)

    val feedingScheduleApi = FeedingScheduleApiImpl(client)
    val feedingScheduleViewModel = FeedingScheduleViewModel(feedingScheduleApi)

    val sensorDataApi = SensorDataApiImpl(client)
    val sensorDataViewModel = SensorDataViewModel(sensorDataApi)

    val navController = rememberNavController()

    NavHost(navController = navController, startDestination = "login") {
        composable("login") {
            LoginPage(
                viewModel = userViewModel,
                onLoginSuccess = { navController.navigate("aquariums") },
                onNavigateToSignUp = { navController.navigate("signup") }
            )
        }
        composable("signup") {
            SignUpPage(
                viewModel = userViewModel,
                onSignUpSuccess = { navController.navigate("aquariums") },
                onNavigateToLogin = { navController.navigate("login") }
            )
        }
        composable("aquariums") {
            AquariumPage(
                userViewModel = userViewModel,
                aquariumViewModel = aquariumViewModel,
                onLogout = { /* Handle logout */ },
                onViewFeedingSchedules = { aquariumId ->
                    navController.navigate("feeding_schedules/$aquariumId")
                },
                onViewSensorData = {
                    navController.navigate("sensor_data")
                }
            )
        }
        composable("feeding_schedules/{aquariumId}") { backStackEntry ->
            val aquariumId = backStackEntry.arguments?.getString("aquariumId")?.toIntOrNull() ?: return@composable
            FeedingSchedulePage(
                aquariumId = aquariumId,
                viewModel = feedingScheduleViewModel,
                onBackClick = { navController.popBackStack() },
                onAddSchedule = { navController.navigate("add_feeding_schedule/$aquariumId") },
                onUpdateSchedule = { schedule ->
                    navController.navigate("update_feeding_schedule/${schedule.feedingScheduleId}")
                }
            )
        }
        composable("add_feeding_schedule/{aquariumId}") { backStackEntry ->
            val aquariumId = backStackEntry.arguments?.getString("aquariumId")?.toIntOrNull() ?: return@composable
            AddFeedingSchedulePage(
                viewModel = feedingScheduleViewModel,
                aquariumId = aquariumId,
                onAddComplete = { navController.popBackStack() }
            )
        }
        composable("update_feeding_schedule/{feedingScheduleId}") { backStackEntry ->
            val feedingScheduleId = backStackEntry.arguments?.getString("feedingScheduleId")?.toIntOrNull() ?: return@composable
            val schedule = feedingScheduleViewModel.fetchFeedingScheduleById(feedingScheduleId)
            schedule?.let {
                UpdateFeedingSchedulePage(
                    viewModel = feedingScheduleViewModel,
                    feedingSchedule = schedule,
                    onUpdateComplete = { navController.popBackStack() }
                )
            }
        }
        composable("sensor_data") {
            SensorDataPage(
                viewModel = sensorDataViewModel,
                onBackClick = { navController.popBackStack() },
                onAddSensorData = { navController.navigate("add_sensor_data") },
                onUpdateSensorData = { sensorData ->
                    navController.navigate("update_sensor_data/${sensorData.sensorDataId}")
                },
                onDeleteSensorData = { sensorDataId ->
                    sensorDataViewModel.deleteSensorData(sensorDataId)
                }
            )
        }

        composable("add_sensor_data") {
            AddSensorDataPage(
                viewModel = sensorDataViewModel,
                onAddComplete = { navController.popBackStack() },
                onBackClick = { navController.popBackStack() }
            )
        }
    }
}

// A generic error page for handling unexpected situations
@Composable
fun ErrorPage(message: String, onBackClick: () -> Unit) {
    Column(
        modifier = Modifier.fillMaxSize().padding(16.dp),
        verticalArrangement = Arrangement.Center,
        horizontalAlignment = Alignment.CenterHorizontally
    ) {
        Text(
            text = "Oops! Something went wrong.",
            style = MaterialTheme.typography.headlineMedium
        )
        Spacer(modifier = Modifier.height(16.dp))
        Text(
            text = message,
            style = MaterialTheme.typography.bodyLarge,
            textAlign = TextAlign.Center
        )
        Spacer(modifier = Modifier.height(32.dp))
        Button(onClick = onBackClick) {
            Text("Go Back")
        }
    }
}