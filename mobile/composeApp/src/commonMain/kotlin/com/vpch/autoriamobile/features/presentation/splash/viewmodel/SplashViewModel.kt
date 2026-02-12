package com.vpch.autoriamobile.features.presentation.splash.viewmodel

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.vpch.autoriamobile.core.data.local.TokenManager
import com.vpch.autoriamobile.features.domain.user.usecase.LoadProfileUseCase
import kotlinx.coroutines.channels.Channel
import kotlinx.coroutines.flow.receiveAsFlow
import kotlinx.coroutines.launch

class SplashViewModel(
    private val tokenManager: TokenManager,
    private val loadProfileUseCase: LoadProfileUseCase
) : ViewModel() {

    private val _effect = Channel<SplashEffect>()
    val effect = _effect.receiveAsFlow()

    init {
        checkSession()
    }

    private fun checkSession() {
        viewModelScope.launch {
            // 1. Перевіряємо, чи є токен локально
            if (tokenManager.isUserLoggedIn()) {
                // 2. Якщо є — пробуємо завантажити свіжий профіль
                val result = loadProfileUseCase()

                result.onSuccess {
                    // Профіль в пам'яті, можна йти додому
                    _effect.send(SplashEffect.NavigateToHome)
                }.onFailure {
                    // Токен є, але сервер вернув помилку (наприклад 401, токен протух)
                    // Або просто немає інтернету.

                    // Тут бізнес-рішення:
                    // Або пустити на Home (хай кеш покаже, якщо є, або пустоту)
                    // Або викинути на логін (безпечніше)

                    // Для надійності, якщо не змогли завантажити юзера - краще на логін,
                    // або лишити на Home, але юзер побачить, що даних нема.
                    // Давай поки пустимо на Home, але ідеально - спробувати рефреш токен.
                    _effect.send(SplashEffect.NavigateToHome)
                }
            } else {
                // Токена немає — на логін
                _effect.send(SplashEffect.NavigateToLogin)
            }
        }
    }
}