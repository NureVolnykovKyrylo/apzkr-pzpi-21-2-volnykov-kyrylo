package com.example.aquatrackmobile.models.FeedingSchedule

import io.ktor.client.*
import io.ktor.client.call.*
import io.ktor.client.request.*
import io.ktor.client.statement.*
import io.ktor.http.*
import kotlinx.serialization.json.*

class FeedingScheduleApiException(message: String, cause: Throwable? = null) : Exception(message, cause)

interface FeedingScheduleApi {
    val client: HttpClient

    suspend fun getFeedingSchedulesByAquariumId(id: Int): List<FeedingSchedule>
    suspend fun getFeedingScheduleById(id: Int): FeedingSchedule
    suspend fun addFeedingScheduleForCurrentUser(feedingSchedule: FeedingSchedule): FeedingSchedule
    suspend fun updateFeedingScheduleForCurrentUser(id: Int, feedingSchedule: FeedingSchedule): FeedingSchedule
    suspend fun deleteFeedingScheduleForCurrentUser(id: Int)
}

class FeedingScheduleApiImpl(override val client: HttpClient) : FeedingScheduleApi {

    private val apiKey: String = "http://0.tcp.eu.ngrok.io:19926/api"

    override suspend fun getFeedingSchedulesByAquariumId(id: Int): List<FeedingSchedule> {
        val response: HttpResponse = client.get("$apiKey/FeedingSchedule/aquarium/$id")
        if (response.status.isSuccess()) {
            return response.body()
        } else {
            throw FeedingScheduleApiException("Failed to fetch feeding schedules by aquarium ID")
        }
    }

    override suspend fun getFeedingScheduleById(id: Int): FeedingSchedule {
        val response: HttpResponse = client.get("$apiKey/FeedingSchedule/$id")
        if (response.status.isSuccess()) {
            return response.body()
        } else {
            throw FeedingScheduleApiException("Failed to fetch feeding schedule by ID")
        }
    }

    override suspend fun addFeedingScheduleForCurrentUser(feedingSchedule: FeedingSchedule): FeedingSchedule {
        val response: HttpResponse = client.post("$apiKey/FeedingSchedule") {
            contentType(ContentType.Application.Json)
            setBody(feedingSchedule)
        }
        if (response.status.isSuccess()) {
            return response.body()
        } else {
            throw FeedingScheduleApiException("Failed to add feeding schedule for current user")
        }
    }

    override suspend fun updateFeedingScheduleForCurrentUser(id: Int, feedingSchedule: FeedingSchedule): FeedingSchedule {
        val response: HttpResponse = client.put("$apiKey/FeedingSchedule/$id") {
            contentType(ContentType.Application.Json)
            setBody(feedingSchedule)
        }
        if (response.status.isSuccess()) {
            return response.body()
        } else {
            throw FeedingScheduleApiException("Failed to update feeding schedule for current user")
        }
    }

    override suspend fun deleteFeedingScheduleForCurrentUser(id: Int) {
        val response: HttpResponse = client.delete("$apiKey/FeedingSchedule/$id")
        if (!response.status.isSuccess()) {
            throw FeedingScheduleApiException("Failed to delete feeding schedule for current user")
        }
    }
}