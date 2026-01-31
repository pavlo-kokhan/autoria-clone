package com.vpch.autoriamobile.features.presentation.auth.registration

sealed interface RegistrationEvent {
    data class OnEmailChange(val email: String) : RegistrationEvent
    data class OnPasswordChange(val password: String) : RegistrationEvent
    data object OnRegisterClick : RegistrationEvent
    data object OnLoginClick : RegistrationEvent
}
