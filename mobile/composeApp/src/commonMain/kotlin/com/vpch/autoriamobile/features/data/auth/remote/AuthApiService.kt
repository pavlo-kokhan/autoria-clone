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
import io.ktor.client.request.post
import io.ktor.client.request.setBody
import io.ktor.client.statement.bodyAsText
import io.ktor.http.ContentType
import io.ktor.http.contentType
import kotlinx.serialization.json.Json

class AuthApiService(
    private val client: HttpClient
) {
    suspend fun register(request: RegisterRequestDto): AuthResponseDto {
        val response = client.post(Constants.BASE_URL + "/auth/register") {
            contentType(ContentType.Application.Json)
            setBody(request)
        }

        val bodyText = response.bodyAsText()
        if (response.status.value in 200..299) {
            return Json.decodeFromString<AuthResponseDto>(bodyText)
        }

        if (response.status == io.ktor.http.HttpStatusCode.BadRequest) {
            try {
                val errorResponse = Json.decodeFromString<AuthErrorResponse>(bodyText)

                if (errorResponse.errors?.containsKey("USER_ALREADY_EXISTS") == true) {
                    throw UserAlreadyExistsException()
                }
            } catch (e: Exception) {
                if (e is UserAlreadyExistsException) throw e
            }
        }

        throw ServerErrorException("Server Error: ${response.status.value} - $bodyText")
    }

    suspend fun login(request: LoginRequestDto): AuthResponseDto {
        val response = client.post(Constants.BASE_URL + "/auth/access-token") {
            contentType(ContentType.Application.Json)
            setBody(request)
        }

        val bodyText = response.bodyAsText()

        if (response.status.value in 200..299) {
            return Json.decodeFromString<AuthResponseDto>(bodyText)
        }

        if (response.status == io.ktor.http.HttpStatusCode.BadRequest) {
            throw InvalidCredentialsException()
        }

        throw ServerErrorException("Server Error: ${response.status.value}")
    }

}
