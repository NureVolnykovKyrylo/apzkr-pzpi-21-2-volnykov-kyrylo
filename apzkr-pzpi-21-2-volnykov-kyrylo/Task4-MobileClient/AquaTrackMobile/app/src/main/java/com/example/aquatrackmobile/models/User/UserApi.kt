package com.example.aquatrackmobile.models.User

import com.example.aquatrackmobile.network.HttpClientProvider
import io.ktor.client.*
import io.ktor.client.call.*
import io.ktor.client.request.*
import io.ktor.client.statement.*
import io.ktor.http.*
import kotlinx.serialization.encodeToString
import kotlinx.serialization.json.*

class LoginException(message: String, cause: Throwable? = null) : Exception(message, cause)
class RegisterException(message: String, cause: Throwable? = null) : Exception(message, cause)

interface UserApi {
    val client: HttpClient

    suspend fun loginUser(email: String, password: String): UserResponse
    suspend fun getCurrentUser(): UserResponse
    suspend fun registerUser(email: String, password: String, username: String): UserResponse
}

class UserApiImpl(override val client: HttpClient = HttpClientProvider.client) : UserApi {

    private val apiKey: String = "http://0.tcp.eu.ngrok.io:19926/api"

    override suspend fun loginUser(email: String, password: String): UserResponse {
        val requestBody = buildJsonObject {
            put("email", email)
            put("password", password)
        }
        println(requestBody)
        try {
            val response: HttpResponse = client.post("$apiKey/user/login") {
                contentType(ContentType.Application.Json)
                setBody(Json.encodeToString(requestBody))
            }

            if (response.status.isSuccess()) {
                return response.body()
            } else {
                throw LoginException("Invalid credentials or other login failure")
            }
        } catch (e: Exception) {
            throw LoginException("Error during login ${e.message}", e)
        }
    }

    override suspend fun getCurrentUser(): UserResponse {
        val response: HttpResponse = client.get("$apiKey/user/userinfo") {
            headers {
                append(HttpHeaders.Accept, "application/json")
            }
        }
        return response.body()
    }

    override suspend fun registerUser(email: String, password: String, username: String): UserResponse {
        val requestBody = buildJsonObject {
            put("email", email)
            put("password", password)
            put("username", username)
        }

        try {
            val response: HttpResponse = client.post("$apiKey/user/register") {
                contentType(ContentType.Application.Json)
                setBody(Json.encodeToString(requestBody))
            }

            if (response.status.isSuccess()) {
                return response.body()
            } else {
                throw LoginException("Registration failed")
            }
        } catch (e: Exception) {
            throw LoginException("Error during registration", e)
        }
    }
}
