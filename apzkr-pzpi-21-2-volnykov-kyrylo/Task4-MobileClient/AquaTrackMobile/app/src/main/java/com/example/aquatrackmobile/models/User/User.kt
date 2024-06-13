package com.example.aquatrackmobile.models.User

import com.example.aquatrackmobile.models.Aquarium.Aquarium
import kotlinx.serialization.Serializable

@Serializable
data class UserResponse(
    val userId: Int,
    val password: String,
    val email: String,
    val phoneNumber: String?,
    val firstName: String,
    val lastName: String,
    val role: Int,
    val aquariums: List<Aquarium>?
)

@Serializable
data class User(
    val userId: Int,
    val password: String,
    val email: String,
    val phoneNumber: String?,
    val firstName: String,
    val lastName: String,
    val role: Int,
    val aquariums: List<Aquarium>?
)

@Serializable
data class UserRegisterRequest(
    val email: String,
    val password: String,
    val name: String
)