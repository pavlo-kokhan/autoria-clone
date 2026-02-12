package com.vpch.autoriamobile.features.data.user.remote

import com.vpch.autoriamobile.Constants
import com.vpch.autoriamobile.features.data.user.dto.UserResponseDto
import com.vpch.autoriamobile.features.domain.auth.exceptions.ServerErrorException
import io.ktor.client.HttpClient
import io.ktor.client.call.body
import io.ktor.client.request.get
import io.ktor.client.request.put
import io.ktor.client.request.setBody
import io.ktor.http.ContentType
import io.ktor.http.contentType
import io.ktor.http.isSuccess

class UserApiService(private val client: HttpClient) {

    suspend fun getProfile(): UserResponseDto {
        val response = client.get(Constants.BASE_URL + "/user") {
            contentType(ContentType.Application.Json)
        }

        if (response.status.isSuccess()) {
            return response.body()
        }

        throw ServerErrorException("Failed to load profile: ${response.status.value}")
    }

//    suspend fun updateContacts(request: UpdateUserContactsRequest): Unit {
//        client.put(Constants.BASE_URL + "/user/contacts") {
//            contentType(ContentType.Application.Json)
//            setBody(request)
//        }
//    }
}