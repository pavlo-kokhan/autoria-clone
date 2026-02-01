package com.vpch.autoriamobile.di

import com.vpch.autoriamobile.features.data.auth.remote.AuthApiService
import com.vpch.autoriamobile.features.data.auth.repository.AuthRepositoryImpl
import com.vpch.autoriamobile.features.domain.auth.repository.AuthRepository
import com.vpch.autoriamobile.features.domain.auth.usecase.LoginUseCase
import com.vpch.autoriamobile.features.domain.auth.usecase.RegisterUseCase
import com.vpch.autoriamobile.features.presentation.auth.login.viewmodel.LoginViewModel
import com.vpch.autoriamobile.features.presentation.auth.registration.viewmodel.RegistrationViewModel
import io.ktor.client.HttpClient
import io.ktor.client.plugins.contentnegotiation.ContentNegotiation
import io.ktor.serialization.kotlinx.json.json
import kotlinx.serialization.json.Json
import org.koin.core.module.dsl.viewModel
import org.koin.dsl.module


val sharedModule = module {
    single {
        HttpClient {
            install(ContentNegotiation) {
                json(Json {
                    ignoreUnknownKeys = true
                    prettyPrint = true
                    isLenient = true
                })
            }
        }
    }
    single { AuthApiService(client = get()) }
    single<AuthRepository> { AuthRepositoryImpl(apiService = get()) }

    factory { RegisterUseCase(repository = get()) }
    factory { LoginUseCase(repository = get()) }

    viewModel { RegistrationViewModel(registerUseCase = get()) }
    viewModel { LoginViewModel(loginUseCase = get()) }
}