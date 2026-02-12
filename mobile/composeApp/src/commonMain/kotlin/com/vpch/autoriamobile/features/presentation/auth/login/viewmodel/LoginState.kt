package com.vpch.autoriamobile.features.presentation.auth.login.viewmodel

import org.jetbrains.compose.resources.StringResource

data class LoginState(
    val email: String = "",
    val password: String = "",
    val emailError: StringResource? = null,
    val passwordError: StringResource? = null,
    val isLoading: Boolean = false,
    val errorRes: StringResource? = null
)
