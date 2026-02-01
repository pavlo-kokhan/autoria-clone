package com.vpch.autoriamobile.features.presentation.auth.registration.viewmodel

import org.jetbrains.compose.resources.StringResource

data class RegistrationState(
    val email: String = "",
    val password: String = "",
    val emailError: StringResource? = null,
    val passwordError: StringResource? = null,
    val isLoading: Boolean = false,
    val errorRes: StringResource? = null
)
