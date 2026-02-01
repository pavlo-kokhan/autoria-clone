package com.vpch.autoriamobile.features.presentation.auth.registration.viewmodel

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.vpch.autoriamobile.core.domain.validation.AuthValidator
import com.vpch.autoriamobile.core.presentation.utils.toUiErrorMessage
import com.vpch.autoriamobile.features.domain.auth.usecase.RegisterUseCase
import kotlinx.coroutines.channels.Channel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.receiveAsFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch

class RegistrationViewModel(
    private val registerUseCase: RegisterUseCase
): ViewModel() {
    private val _state = MutableStateFlow(RegistrationState())
    val state = _state.asStateFlow()

    private val _effect = Channel<RegistrationEffect>()
    val effect = _effect.receiveAsFlow()

    fun onEvent(event: RegistrationEvent) {
        when (event) {
            is RegistrationEvent.OnEmailChange -> {
                _state.update { it.copy(email = event.email, emailError = null, errorRes = null) }
            }
            is RegistrationEvent.OnPasswordChange -> {
                _state.update { it.copy(password = event.password, passwordError = null, errorRes = null) }
            }
            is RegistrationEvent.OnRegisterClick -> {
                register()
            }
            is RegistrationEvent.OnLoginClick -> {
                sendEffect(RegistrationEffect.NavigateToLogin)
            }
        }
    }

    private fun register() {
        val currentState = _state.value
        val emailError = AuthValidator.validateEmail(currentState.email)
        val passwordError = AuthValidator.validatePassword(currentState.password)

        if (emailError != null || passwordError != null) {
            _state.update {
                it.copy(
                    emailError = emailError,
                    passwordError = passwordError
                )
            }
            return
        }

        viewModelScope.launch {
            _state.update { it.copy(isLoading = true, errorRes = null) }

            val result = registerUseCase(
                email = currentState.email,
                password = currentState.password
            )

            result.onSuccess {
                _state.update { it.copy(isLoading = false) }
                sendEffect(RegistrationEffect.NavigateToHome)
            }.onFailure { error ->
                _state.update {
                    it.copy(
                        isLoading = false,
                        errorRes = error.toUiErrorMessage()
                    )
                }
            }
        }
    }

    private fun sendEffect(effect: RegistrationEffect) {
        viewModelScope.launch {
            _effect.send(effect)
        }
    }
}