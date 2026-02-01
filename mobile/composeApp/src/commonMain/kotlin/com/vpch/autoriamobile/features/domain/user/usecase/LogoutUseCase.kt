package com.vpch.autoriamobile.features.domain.user.usecase

import com.vpch.autoriamobile.core.data.local.TokenManager
import com.vpch.autoriamobile.features.domain.user.repository.UserRepository

class LogoutUseCase(
    private val tokenManager: TokenManager,
    private val userRepository: UserRepository
) {
    operator fun invoke() {
        tokenManager.clearTokens()
        userRepository.clearUserData()
    }
}