package com.vpch.autoriamobile.di

import com.vpch.autoriamobile.features.data.user.remote.UserApiService
import com.vpch.autoriamobile.features.data.user.repository.UserRepositoryImpl
import com.vpch.autoriamobile.features.domain.user.repository.UserRepository
import com.vpch.autoriamobile.features.domain.user.usecase.LoadProfileUseCase
import com.vpch.autoriamobile.features.domain.user.usecase.ObserveUserUseCase
import com.vpch.autoriamobile.features.presentation.home.viewmodel.HomeViewModel
import com.vpch.autoriamobile.features.presentation.splash.viewmodel.SplashViewModel
import org.koin.core.module.dsl.viewModel
import org.koin.dsl.module

val userModule = module {
    single { UserApiService(client = get()) }
    single<UserRepository> { UserRepositoryImpl(apiService = get()) }

    factory { ObserveUserUseCase(userRepository = get()) }
    factory { LoadProfileUseCase(userRepository = get()) }

    viewModel { SplashViewModel(tokenManager = get(), loadProfileUseCase = get()) }
    viewModel { HomeViewModel(observeUserUseCase = get(), logoutUseCase = get()) }
}