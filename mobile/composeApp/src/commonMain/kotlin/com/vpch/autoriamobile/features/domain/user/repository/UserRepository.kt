package com.vpch.autoriamobile.features.domain.user.repository

import com.vpch.autoriamobile.features.domain.user.model.User
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.StateFlow

interface UserRepository {
    val currentUser: StateFlow<User?>
    suspend fun loadProfile(): Result<Unit>
    fun clearUserData()
}