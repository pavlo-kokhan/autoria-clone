package com.vpch.autoriamobile.features.data.auth.mappers

import com.vpch.autoriamobile.features.data.auth.dto.AuthResponseDto
import com.vpch.autoriamobile.features.domain.auth.model.AuthToken

fun AuthResponseDto.toAuthToken(): AuthToken = AuthToken(
    accessToken = accessToken,
    refreshToken = refreshToken
)