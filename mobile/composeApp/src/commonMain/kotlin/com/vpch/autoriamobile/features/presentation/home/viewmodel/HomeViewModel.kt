package com.vpch.autoriamobile.features.presentation.home.viewmodel

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.vpch.autoriamobile.features.domain.user.usecase.LogoutUseCase
import com.vpch.autoriamobile.features.domain.user.usecase.ObserveUserUseCase
import kotlinx.coroutines.channels.Channel
import kotlinx.coroutines.flow.receiveAsFlow
import kotlinx.coroutines.launch

class HomeViewModel(
    private val observeUserUseCase: ObserveUserUseCase,
    private val logoutUseCase: LogoutUseCase
) : ViewModel() {

    val user = observeUserUseCase()

    private val _effect = Channel<HomeEffect>()
    val effect = _effect.receiveAsFlow()

    fun onEvent(event: HomeEvent) {
        when (event) {
            is HomeEvent.OnLogoutClick -> logout()
        }
    }

    private fun logout() {
        viewModelScope.launch {
            logoutUseCase()
            _effect.send(HomeEffect.NavigateToLogin)
        }
    }
}
