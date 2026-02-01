package com.vpch.autoriamobile.features.presentation.home.viewmodel

sealed interface HomeEffect {
    data object NavigateToLogin : HomeEffect
}