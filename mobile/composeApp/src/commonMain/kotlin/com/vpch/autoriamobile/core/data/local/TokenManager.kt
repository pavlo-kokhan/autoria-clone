package com.vpch.autoriamobile.core.data.local

import com.russhwolf.settings.Settings


class TokenManager(
    private val settings: Settings
) {
    companion object {
        private const val ACCESS_TOKEN_KEY = "access_token"
        private const val REFRESH_TOKEN_KEY = "refresh_token"
    }

    fun saveTokens(accessToken: String, refreshToken: String) {
        settings.putString(ACCESS_TOKEN_KEY, accessToken)
        settings.putString(REFRESH_TOKEN_KEY, refreshToken)
    }

    fun getAccessToken(): String? {
        return settings.getStringOrNull(ACCESS_TOKEN_KEY)
    }

    fun getRefreshToken(): String? {
        return settings.getStringOrNull(REFRESH_TOKEN_KEY)
    }

    fun clearTokens() {
        settings.remove(ACCESS_TOKEN_KEY)
        settings.remove(REFRESH_TOKEN_KEY)
    }

    fun isUserLoggedIn(): Boolean {
        return settings.getStringOrNull(ACCESS_TOKEN_KEY) != null
    }
}