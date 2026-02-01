package com.vpch.autoriamobile.features.data.user.repository

import com.vpch.autoriamobile.features.data.user.mappers.toDomain
import com.vpch.autoriamobile.features.data.user.remote.UserApiService
import com.vpch.autoriamobile.features.domain.user.model.User
import com.vpch.autoriamobile.features.domain.user.repository.UserRepository
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow

class UserRepositoryImpl(
    private val apiService: UserApiService
) : UserRepository {

    private val _currentUser = MutableStateFlow<User?>(null)
    override val currentUser = _currentUser.asStateFlow()

    override suspend fun loadProfile(): Result<Unit> {
        return try {
            val dto = apiService.getProfile()
            val user = dto.toDomain()

            _currentUser.value = user
            Result.success(Unit)
        } catch (e: Exception) {
            Result.failure(e)
        }
    }

    override fun clearUserData() {
        _currentUser.value = null
    }
}