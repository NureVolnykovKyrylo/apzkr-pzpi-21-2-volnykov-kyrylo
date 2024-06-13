package com.example.aquatrackmobile.viewmodels

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.aquatrackmobile.models.Aquarium.AquariumApi
import com.example.aquatrackmobile.models.Aquarium.AquariumApiException
import com.example.aquatrackmobile.models.Aquarium.Aquarium
import kotlinx.coroutines.flow.*
import kotlinx.coroutines.launch

class AquariumViewModel(
    private val aquariumApi: AquariumApi
) : ViewModel() {

    private val _selectedAquarium = MutableStateFlow<Aquarium?>(null)
    val selectedAquarium: StateFlow<Aquarium?> = _selectedAquarium.asStateFlow()

    private val _userAquariums = MutableStateFlow<List<Aquarium>>(emptyList())
    val userAquariums: StateFlow<List<Aquarium>> = _userAquariums.asStateFlow()

    private val _isLoading = MutableStateFlow(false)
    val isLoading: StateFlow<Boolean> = _isLoading.asStateFlow()

    private val _error = MutableSharedFlow<String>()
    val error: SharedFlow<String> = _error.asSharedFlow()

    fun fetchAquariumById(id: Int) = viewModelScope.launch {
        try {
            _isLoading.value = true
            val aquarium = aquariumApi.getAquariumById(id)
            _selectedAquarium.value = aquarium
            _isLoading.value = false
        } catch (e: AquariumApiException) {
            _error.emit("Failed to fetch aquarium: ${e.message}")
            _isLoading.value = false
        } catch (e: Exception) {
            _error.emit("Error: ${e.message}")
            _isLoading.value = false
        }
    }

    fun fetchAquariumsForUser(userId: Int) = viewModelScope.launch {
        try {
            _isLoading.value = true
            val aquariums = aquariumApi.getAquariumsForUser(userId)
            _userAquariums.value = aquariums
            _isLoading.value = false
            println(_userAquariums.value)
        } catch (e: AquariumApiException) {
            _error.emit("Failed to fetch user's aquariums: ${e.message}")
            _isLoading.value = false
        } catch (e: Exception) {
            _error.emit("Error: ${e.message}")
            _isLoading.value = false
        }
    }

    fun addAquariumForUser(aquarium: Aquarium, userId: Int) = viewModelScope.launch {
        try {
            _isLoading.value = true
            // Uncomment when the API is ready
            // val newAquarium = aquariumApi.addAquariumForUser(aquarium, userId)
            // val currentList = _userAquariums.value.toMutableList()
            // currentList.add(newAquarium)
            // _userAquariums.value = currentList
            _isLoading.value = false
        } catch (e: AquariumApiException) {
            _error.emit("Failed to add aquarium: ${e.message}")
            _isLoading.value = false
        } catch (e: Exception) {
            _error.emit("Error: ${e.message}")
            _isLoading.value = false
        }
    }

    fun updateAquariumForUser(id: Int, aquarium: Aquarium, userId: Int) = viewModelScope.launch {
        try {
            _isLoading.value = true
            // Uncomment when the API is ready
            // val updatedAquarium = aquariumApi.updateAquariumForUser(id, aquarium, userId)
            // val currentList = _userAquariums.value.toMutableList()
            // val index = currentList.indexOfFirst { it.id == id }
            // if (index != -1) {
            //     currentList[index] = updatedAquarium
            //     _userAquariums.value = currentList
            // }
            _isLoading.value = false
        } catch (e: AquariumApiException) {
            _error.emit("Failed to update aquarium: ${e.message}")
            _isLoading.value = false
        } catch (e: Exception) {
            _error.emit("Error: ${e.message}")
            _isLoading.value = false
        }
    }

    fun deleteAquariumForUser(id: Int, userId: Int) = viewModelScope.launch {
        try {
            _isLoading.value = true
            // Uncomment when the API is ready
            // aquariumApi.deleteAquariumForUser(id, userId)
            // val currentList = _userAquariums.value.toMutableList()
            // currentList.removeAll { it.id == id }
            // _userAquariums.value = currentList
            _isLoading.value = false
        } catch (e: AquariumApiException) {
            _error.emit("Failed to delete aquarium: ${e.message}")
            _isLoading.value = false
        } catch (e: Exception) {
            _error.emit("Error: ${e.message}")
            _isLoading.value = false
        }
    }

    // Helper function to select an aquarium from the list
    fun selectAquarium(aquarium: Aquarium) {
        _selectedAquarium.value = aquarium
    }

    // Helper function to clear the selected aquarium
    fun clearSelectedAquarium() {
        _selectedAquarium.value = null
    }
}

