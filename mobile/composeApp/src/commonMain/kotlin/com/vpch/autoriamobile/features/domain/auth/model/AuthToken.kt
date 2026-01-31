package com.vpch.autoriamobile.features.domain.auth.model

data class AuthToken(
    val accessToken: String,
    val refreshToken: String
)
