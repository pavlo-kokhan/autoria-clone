package com.vpch.autoriamobile.features.data.auth.repository

import com.vpch.autoriamobile.features.data.auth.dto.RegisterRequestDto
import com.vpch.autoriamobile.features.data.auth.mappers.toAuthToken
import com.vpch.autoriamobile.features.data.auth.remote.AuthApiService
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
            val requestDto = RegisterRequestDto(email = email, password = password)
            val responseDto = apiService.register(requestDto)

            val domainModel = responseDto.toAuthToken()
            Result.success(domainModel)
        } catch (e: UserAlreadyExistsException) {
            Result.failure(e)
        } catch (e: Exception) {
            e.printStackTrace()
            Result.failure(e)
        }
    }
}