package com.example.aquatrackmobile.viewmodels

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.aquatrackmobile.models.FeedingSchedule.FeedingScheduleApi
import com.example.aquatrackmobile.models.FeedingSchedule.FeedingSchedule
import kotlinx.coroutines.launch
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow

class FeedingScheduleViewModel(private val feedingScheduleApi: FeedingScheduleApi) : ViewModel() {
    private val _feedingScheduleList = MutableStateFlow<List<FeedingSchedule>>(emptyList())
    val feedingScheduleList: StateFlow<List<FeedingSchedule>> = _feedingScheduleList

    private val _feedingSchedule = MutableStateFlow<FeedingSchedule?>(null)
    val feedingSchedule: StateFlow<FeedingSchedule?> = _feedingSchedule

    fun fetchFeedingSchedulesByAquariumId(id: Int) {
        viewModelScope.launch {
            try {
                _feedingScheduleList.value = feedingScheduleApi.getFeedingSchedulesByAquariumId(id)
            } catch (e: Exception) {
                // Handle error
            }
        }
    }

    fun fetchFeedingScheduleById(id: Int): FeedingSchedule? {
        viewModelScope.launch {
            try {
                _feedingSchedule.value = feedingScheduleApi.getFeedingScheduleById(id)
            } catch (e: Exception) {
                // Handle error
            }
        }
        return _feedingSchedule.value
    }

    fun addFeedingScheduleForCurrentUser(feedingSchedule: FeedingSchedule) {
        viewModelScope.launch {
            try {
                _feedingSchedule.value = feedingScheduleApi.addFeedingScheduleForCurrentUser(feedingSchedule)
            } catch (e: Exception) {
                // Handle error
            }
        }
    }

    fun updateFeedingScheduleForCurrentUser(id: Int, feedingScheduleUpdate: FeedingSchedule) {
        viewModelScope.launch {
            try {
                _feedingSchedule.value = feedingScheduleApi.updateFeedingScheduleForCurrentUser(id, feedingScheduleUpdate)
            } catch (e: Exception) {
                // Handle error
            }
        }
    }

    fun deleteFeedingScheduleForCurrentUser(id: Int, aquariumId: Int) {
        viewModelScope.launch {
            try {
                feedingScheduleApi.deleteFeedingScheduleForCurrentUser(id)
                fetchFeedingScheduleById(aquariumId) // Refresh the list after deletion
            } catch (e: Exception) {
                // Handle error
            }
        }
    }
}


