package com.example.aquatrackmobile.models.Aquarium

import io.ktor.client.*
import io.ktor.client.call.*
import io.ktor.client.request.*
import io.ktor.client.statement.*
import io.ktor.http.*
import com.example.aquatrackmobile.models.Aquarium.Aquarium

class AquariumApiException(message: String, cause: Throwable? = null) : Exception(message, cause)

interface AquariumApi {
    val client: HttpClient

    suspend fun getAquariumById(id: Int): Aquarium
    suspend fun getAquariumsForUser(userId: Int): List<Aquarium>

}

class AquariumApiImpl(override val client: HttpClient) : AquariumApi {

    private val apiKey: String = "http://0.tcp.eu.ngrok.io:19926/api"

    override suspend fun getAquariumById(id: Int): Aquarium {
        val response: HttpResponse = client.get("$apiKey/Aquarium/$id")
        if (response.status.isSuccess()) {
            return response.body()
        } else {
            throw AquariumApiException("Failed to fetch aquarium by ID")
        }
    }

    override suspend fun getAquariumsForUser(userId: Int): List<Aquarium> {
        val response: HttpResponse = client.get("$apiKey/Aquarium/user/$userId")
        if (response.status.isSuccess()) {
            return response.body()
        } else {
            throw AquariumApiException("Failed to fetch aquariums for user")
        }
    }

}
