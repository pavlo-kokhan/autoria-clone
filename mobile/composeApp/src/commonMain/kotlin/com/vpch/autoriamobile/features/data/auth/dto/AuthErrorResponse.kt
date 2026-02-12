package com.vpch.autoriamobile.features.data.auth.dto

import kotlinx.serialization.SerialName
import kotlinx.serialization.Serializable

@Serializable
data class AuthErrorResponse(
    @SerialName("errors") val errors: Map<String, String?>?,
    @SerialName("resultStatus") val resultStatus: String?
)
