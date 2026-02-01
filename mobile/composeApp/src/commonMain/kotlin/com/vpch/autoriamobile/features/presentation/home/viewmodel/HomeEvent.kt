package com.vpch.autoriamobile.features.presentation.home.viewmodel

sealed interface HomeEvent {
    data object OnLogoutClick : HomeEvent
}