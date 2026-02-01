package com.vpch.autoriamobile.features.presentation.auth.registration.viewmodel

sealed interface RegistrationEffect {
    data object NavigateToHome : RegistrationEffect
    data object NavigateToLogin : RegistrationEffect
}