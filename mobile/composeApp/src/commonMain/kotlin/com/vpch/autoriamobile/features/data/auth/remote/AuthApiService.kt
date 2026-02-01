package com.vpch.autoriamobile.features.data.auth.remote

import com.vpch.autoriamobile.Constants
import com.vpch.autoriamobile.features.data.auth.dto.AuthErrorResponse
import com.vpch.autoriamobile.features.data.auth.dto.AuthResponseDto
import com.vpch.autoriamobile.features.data.auth.dto.LoginRequestDto
import com.vpch.autoriamobile.features.data.auth.dto.RegisterRequestDto
import com.vpch.autoriamobile.features.domain.auth.exceptions.InvalidCredentialsException
import com.vpch.autoriamobile.features.domain.auth.exceptions.ServerErrorException
import com.vpch.autoriamobile.features.domain.auth.exceptions.UserAlreadyExistsException
import io.ktor.client.HttpClient
import io.ktor.client.call.body
import io.ktor.client.request.post
import io.ktor.client.request.setBody
import io.ktor.client.statement.bodyAsText
import io.ktor.http.ContentType
import io.ktor.http.HttpStatusCode
import io.ktor.http.contentType
import io.ktor.http.isSuccess
import kotlinx.serialization.json.Json

class AuthApiService(
    private val client: HttpClient
) {
    suspend fun register(request: RegisterRequestDto): AuthResponseDto {
        val response = client.post(Constants.BASE_URL + "/auth/register") {
            contentType(ContentType.Application.Json)
            setBody(request)
        }

        if (response.status.isSuccess()) {
            return response.body()
        }

        if (response.status == HttpStatusCode.BadRequest) {
            val errorBody = runCatching { response.body<AuthErrorResponse>() }.getOrNull()

            if (errorBody?.errors?.containsKey("USER_ALREADY_EXISTS") == true) {
                throw UserAlreadyExistsException()
            }
        }

        throw ServerErrorException("Server Error: ${response.status.value}")
    }

    suspend fun login(request: LoginRequestDto): AuthResponseDto {
        val response = client.post(Constants.BASE_URL + "/auth/access-token") {
            contentType(ContentType.Application.Json)
            setBody(request)
        }

        if (response.status.isSuccess()) {
            return response.body()
        }

        if (response.status == HttpStatusCode.BadRequest) {
            throw InvalidCredentialsException()
        }

        throw ServerErrorException("Server Error: ${response.status.value}")
    }

}
