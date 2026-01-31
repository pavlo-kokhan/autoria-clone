package com.vpch.autoriamobile.features.domain.auth.repository


import com.vpch.autoriamobile.features.domain.auth.model.AuthToken

interface AuthRepository {
    suspend fun register(email: String, password: String): Result<AuthToken>
}