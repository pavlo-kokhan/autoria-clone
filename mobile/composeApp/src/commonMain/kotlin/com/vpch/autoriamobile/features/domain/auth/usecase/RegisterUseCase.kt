package com.vpch.autoriamobile.features.domain.auth.usecase

import com.vpch.autoriamobile.features.domain.auth.repository.AuthRepository

class RegisterUseCase(
    private val repository: AuthRepository
) {
    suspend operator fun invoke(email: String, password: String): Result<Unit> {
        if (password.length < 8) {
            return Result.failure(Exception("Password too short"))
        }
        if (email.length > 32) {
            return Result.failure(Exception("Email too long"))
        }

        val result = repository.register(email, password)

        return result.map { tokens ->
            Unit
        }
    }
}