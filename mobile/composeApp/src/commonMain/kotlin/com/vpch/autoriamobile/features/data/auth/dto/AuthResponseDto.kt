package com.vpch.autoriamobile.features.data.auth.dto

import kotlinx.serialization.SerialName
import kotlinx.serialization.Serializable

@Serializable
data class AuthResponseDto(
    @SerialName("accessToken") val accessToken: String,
    @SerialName("refreshToken") val refreshToken: String,
    @SerialName("accessTokenExpiration") val accessTokenExpiration: String,
    @SerialName("refreshTokenExpiration") val refreshTokenExpiration: String
)
