package com.vpch.autoriamobile.features.domain.user.usecase

import com.vpch.autoriamobile.features.domain.user.repository.UserRepository

class LoadProfileUseCase(private val userRepository: UserRepository) {
    suspend operator fun invoke(): Result<Unit> {
        return userRepository.loadProfile()
    }
}