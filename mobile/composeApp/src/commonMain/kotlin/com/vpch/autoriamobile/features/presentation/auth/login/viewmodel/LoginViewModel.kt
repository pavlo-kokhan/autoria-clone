package com.vpch.autoriamobile.features.presentation.auth.login.viewmodel

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.vpch.autoriamobile.core.domain.validation.AuthValidator
import com.vpch.autoriamobile.core.presentation.utils.toUiErrorMessage
import com.vpch.autoriamobile.features.domain.auth.usecase.LoginUseCase
import com.vpch.autoriamobile.features.domain.user.usecase.LoadProfileUseCase
import kotlinx.coroutines.channels.Channel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.receiveAsFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch

class LoginViewModel(
    private val loginUseCase: LoginUseCase,
    private val loadProfileUseCase: LoadProfileUseCase
): ViewModel() {
    private val _state = MutableStateFlow(LoginState())
    val state = _state.asStateFlow()

    private val _effect = Channel<LoginEffect>()
    val effect = _effect.receiveAsFlow()

    fun onEvent(event: LoginEvent) {
        when (event) {
            is LoginEvent.OnEmailChange -> {
                _state.update { it.copy(email = event.email, emailError = null, errorRes = null) }
            }
            is LoginEvent.OnPasswordChange -> {
                _state.update { it.copy(password = event.password, passwordError = null, errorRes = null) }
            }
            is LoginEvent.OnLoginClick -> {
                login()
            }
            is LoginEvent.OnRegisterClick -> {
                sendEffect(LoginEffect.NavigateToRegistration)
            }
        }
    }

    private fun login() {
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

            val result = loginUseCase(
                email = currentState.email,
                password = currentState.password
            )

            result.onSuccess {
                val profileResult = loadProfileUseCase()
                profileResult.onSuccess {
                    _state.update { it.copy(isLoading = false) }
                    sendEffect(LoginEffect.NavigateToHome)
                }.onFailure {
                    _state.update { it.copy(isLoading = false) }
                    sendEffect(LoginEffect.NavigateToHome)
                }
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


    private fun sendEffect(effect: LoginEffect) {
        viewModelScope.launch {
            _effect.send(effect)
        }
    }
}