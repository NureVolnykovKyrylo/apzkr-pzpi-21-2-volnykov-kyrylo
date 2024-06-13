package com.example.aquatrackmobile.viewmodels

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.aquatrackmobile.models.User.User
import com.example.aquatrackmobile.models.User.UserResponse
import com.example.aquatrackmobile.models.User.LoginException
import com.example.aquatrackmobile.models.User.RegisterException
import com.example.aquatrackmobile.models.User.UserApi
import kotlinx.coroutines.flow.MutableSharedFlow
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.SharedFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asSharedFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.launch

class UserViewModel(
    private val userApi: UserApi
) : ViewModel() {

    private val _user = MutableStateFlow<UserResponse?>(null)
    val user: StateFlow<UserResponse?> = _user.asStateFlow()

    private val _isLoading = MutableStateFlow(false)
    val isLoading: StateFlow<Boolean> = _isLoading.asStateFlow()

    private val _error = MutableSharedFlow<String>()
    val error: SharedFlow<String> = _error.asSharedFlow()

    fun loginUser(email: String, password: String) = viewModelScope.launch {
        try {
            _isLoading.value = true
            val response: UserResponse = userApi.loginUser(email, password)
            _user.value = response // This line is crucial
            _isLoading.value = false
            println(response)
        } catch (e: LoginException) {
            _error.emit("Login failed: ${e}")
            _isLoading.value = false
            println(e.message)
        } catch (e: Exception) {
            _error.emit("Error during login: ${e}")
            _isLoading.value = false
            println(e.message)
        }
    }

    fun registerUser(email: String, password: String, name: String) = viewModelScope.launch {
        try {
            _isLoading.value = true
            val response: UserResponse = userApi.registerUser(email, password, name)
            _user.value = response
            _isLoading.value = false
        } catch (e: RegisterException) {
            _error.emit("Registration failed: ${e.message}")
            _isLoading.value = false
        } catch (e: Exception) {
            _error.emit("Error during registration: ${e.message}")
            _isLoading.value = false
        }
    }

    fun logoutUser() {
        _user.value = null
    }

    fun refreshUserDetails() = viewModelScope.launch {
        try {
            _isLoading.value = true
            _isLoading.value = false
        } catch (e: Exception) {
            _error.emit("Error refreshing user details: ${e.message}")
            _isLoading.value = false
        }
    }

    fun getCurrentUser(): UserResponse? {
        return _user.value
    }

}