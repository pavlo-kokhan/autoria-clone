package com.vpch.autoriamobile.features.presentation.auth.registration

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import autoriamobile.composeapp.generated.resources.Res
import autoriamobile.composeapp.generated.resources.error_incorrect_email
import autoriamobile.composeapp.generated.resources.error_short_password
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

        val isEmailValid = validateEmail(currentState.email)
        val isPasswordValid = validatePassword(currentState.password)

        if (!isEmailValid || !isPasswordValid) return

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

    private fun validateEmail(email: String): Boolean {
        return if (email.isBlank() || !email.contains("@")) {
            _state.update { it.copy(emailError = Res.string.error_incorrect_email) }
            false
        } else {
            true
        }
    }

    private fun validatePassword(password: String): Boolean {
        return if (password.length < 8) {
            _state.update { it.copy(passwordError = Res.string.error_short_password) }
            false
        } else {
            true
        }
    }

    private fun sendEffect(effect: RegistrationEffect) {
        viewModelScope.launch {
            _effect.send(effect)
        }
    }
}