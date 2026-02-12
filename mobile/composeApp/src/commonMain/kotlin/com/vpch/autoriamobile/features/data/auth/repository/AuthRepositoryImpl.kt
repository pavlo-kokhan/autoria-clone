package com.vpch.autoriamobile.features.data.auth.repository

import com.vpch.autoriamobile.features.data.auth.dto.LoginRequestDto
import com.vpch.autoriamobile.features.data.auth.dto.RegisterRequestDto
import com.vpch.autoriamobile.features.data.auth.mappers.toAuthToken
import com.vpch.autoriamobile.features.data.auth.remote.AuthApiService
import com.vpch.autoriamobile.features.domain.auth.exceptions.InvalidCredentialsException
import com.vpch.autoriamobile.features.domain.auth.exceptions.UserAlreadyExistsException
import com.vpch.autoriamobile.features.domain.auth.model.AuthToken
import com.vpch.autoriamobile.features.domain.auth.repository.AuthRepository

class AuthRepositoryImpl(
    private val apiService: AuthApiService
) : AuthRepository {
    override suspend fun register(
        email: String,
        password: String
    ): Result<AuthToken> {
        return try {
            val request = RegisterRequestDto(email = email, password = password)
            val response = apiService.register(request)

            Result.success(response.toAuthToken())
        } catch (e: UserAlreadyExistsException) {
            Result.failure(e)
        } catch (e: Exception) {
            e.printStackTrace()
            Result.failure(e)
        }
    }

    override suspend fun login(
        email: String,
        password: String
    ): Result<AuthToken> {
        return try {
            val request = LoginRequestDto(email, password)
            val response = apiService.login(request)

            Result.success(response.toAuthToken())
        } catch (e: InvalidCredentialsException) {
            Result.failure(e)
        } catch (e: Exception) {
            e.printStackTrace()
            Result.failure(e)
        }
    }
}