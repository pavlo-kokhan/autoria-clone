package com.vpch.autoriamobile.features.presentation.auth.login.viewmodel

sealed interface LoginEffect {
    data object NavigateToHome : LoginEffect
    data object NavigateToRegistration : LoginEffect
}