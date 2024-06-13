package com.example.aquatrackmobile.models.FeedingSchedule

import kotlinx.serialization.Serializable

@Serializable
data class FeedingScheduleResponse(
    val feedingSchedule: FeedingSchedule
)

@Serializable
data class FeedingSchedule(
    val feedingScheduleId: Int,
    val aquariumId: Int,
    val feedTime: String,
    val feedAmount: String,
    val feedType: String,
    val repeatInterval: String,
    val active: Boolean
)
