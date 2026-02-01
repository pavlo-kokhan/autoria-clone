package com.vpch.autoriamobile.di

import com.vpch.autoriamobile.features.data.auth.remote.AuthApiService
import com.vpch.autoriamobile.features.data.auth.repository.AuthRepositoryImpl
import com.vpch.autoriamobile.features.domain.auth.repository.AuthRepository
import com.vpch.autoriamobile.features.domain.auth.usecase.LoginUseCase
import com.vpch.autoriamobile.features.domain.auth.usecase.RegisterUseCase
import com.vpch.autoriamobile.features.domain.user.usecase.LogoutUseCase
import com.vpch.autoriamobile.features.presentation.auth.login.viewmodel.LoginViewModel
import com.vpch.autoriamobile.features.presentation.auth.registration.viewmodel.RegistrationViewModel
import org.koin.core.module.dsl.viewModel
import org.koin.dsl.module

val authModule = module {
    single { AuthApiService(client = get()) }
    single<AuthRepository> { AuthRepositoryImpl(apiService = get()) }

    factory { RegisterUseCase(repository = get(), tokenManager = get()) }
    factory { LoginUseCase(repository = get(), tokenManager = get()) }
    factory { LogoutUseCase(tokenManager = get(), userRepository = get()) } // Logout тут, бо це керування сесією

    viewModel { RegistrationViewModel(registerUseCase = get(), loadProfileUseCase = get()) }
    viewModel { LoginViewModel(loginUseCase = get(), loadProfileUseCase = get()) }
}