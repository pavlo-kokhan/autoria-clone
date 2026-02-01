package com.vpch.autoriamobile.features.presentation.auth.login.viewmodel

sealed interface LoginEvent {
    data class OnEmailChange(val email: String) : LoginEvent
    data class OnPasswordChange(val password: String) : LoginEvent
    data object OnRegisterClick : LoginEvent
    data object OnLoginClick : LoginEvent
}
