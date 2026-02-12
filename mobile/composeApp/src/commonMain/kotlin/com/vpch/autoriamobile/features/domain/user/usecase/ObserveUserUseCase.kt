package com.vpch.autoriamobile.features.domain.user.usecase

import com.vpch.autoriamobile.features.domain.user.model.User
import com.vpch.autoriamobile.features.domain.user.repository.UserRepository
import kotlinx.coroutines.flow.StateFlow

class ObserveUserUseCase(
    private val userRepository: UserRepository
) {
    operator fun invoke(): StateFlow<User?> {
        return userRepository.currentUser
    }
}