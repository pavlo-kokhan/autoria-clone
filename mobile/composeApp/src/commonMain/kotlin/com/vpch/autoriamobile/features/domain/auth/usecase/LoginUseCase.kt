package com.vpch.autoriamobile.features.domain.auth.usecase

import com.vpch.autoriamobile.core.data.local.TokenManager
import com.vpch.autoriamobile.core.domain.validation.AuthSpecs
import com.vpch.autoriamobile.features.domain.auth.repository.AuthRepository

class LoginUseCase(
    private val repository: AuthRepository,
    private val tokenManager: TokenManager
) {
    suspend operator fun invoke(email: String, password: String): Result<Unit> {
        if (!AuthSpecs.isEmailValid(email)) {
            return Result.failure(Exception("Invalid email format"))
        }

        if (!AuthSpecs.isPasswordValid(password)) {
            return Result.failure(Exception("Password validation failed"))
        }

        val result = repository.login(email, password)

        return result.map { tokens ->
            tokenManager.saveTokens(
                accessToken = tokens.accessToken,
                refreshToken = tokens.refreshToken
            )
        }
    }
}