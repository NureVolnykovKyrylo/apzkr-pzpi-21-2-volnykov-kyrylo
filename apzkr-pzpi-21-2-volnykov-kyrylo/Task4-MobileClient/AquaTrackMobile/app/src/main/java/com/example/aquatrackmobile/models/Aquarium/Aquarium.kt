package com.example.aquatrackmobile.models.Aquarium

import com.example.aquatrackmobile.models.FeedingSchedule.FeedingSchedule
import kotlinx.serialization.Serializable

@Serializable
data class AquariumResponse(
    val aquarium: Aquarium
)

@Serializable
data class Aquarium(
    val aquariumId: Int,
    val aquariumType: String,
    val acidity: Double,
    val waterLevel: Double,
    val temperature: Double,
    val lighting: String
)