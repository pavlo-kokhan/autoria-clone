package com.vpch.autoriamobile.features.data.user.dto

import kotlinx.serialization.SerialName
import kotlinx.serialization.Serializable

@Serializable
data class UserResponseDto(
    @SerialName("email") val email: String,
    @SerialName("firstName") val firstName: String?,
    @SerialName("lastName") val lastName: String?,
    @SerialName("phoneNumber") val phoneNumber: String?,
    @SerialName("telegramUserName") val telegramUserName: String?
)
