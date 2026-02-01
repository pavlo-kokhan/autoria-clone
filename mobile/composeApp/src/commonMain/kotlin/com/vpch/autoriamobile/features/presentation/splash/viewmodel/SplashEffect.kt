package com.vpch.autoriamobile.features.presentation.splash.viewmodel

sealed interface SplashEffect {
    data object NavigateToHome : SplashEffect
    data object NavigateToLogin : SplashEffect
}