package com.vpch.autoriamobile.di



import com.russhwolf.settings.Settings
import com.vpch.autoriamobile.core.data.local.TokenManager
import com.vpch.autoriamobile.features.data.auth.remote.AuthApiService
import com.vpch.autoriamobile.features.data.auth.repository.AuthRepositoryImpl
import com.vpch.autoriamobile.features.data.user.remote.UserApiService
import com.vpch.autoriamobile.features.data.user.repository.UserRepositoryImpl
import com.vpch.autoriamobile.features.domain.auth.repository.AuthRepository
import com.vpch.autoriamobile.features.domain.auth.usecase.LoginUseCase
import com.vpch.autoriamobile.features.domain.auth.usecase.RegisterUseCase
import com.vpch.autoriamobile.features.domain.user.repository.UserRepository
import com.vpch.autoriamobile.features.domain.user.usecase.LoadProfileUseCase
import com.vpch.autoriamobile.features.domain.user.usecase.LogoutUseCase
import com.vpch.autoriamobile.features.domain.user.usecase.ObserveUserUseCase
import com.vpch.autoriamobile.features.presentation.auth.login.viewmodel.LoginViewModel
import com.vpch.autoriamobile.features.presentation.auth.registration.viewmodel.RegistrationViewModel
import com.vpch.autoriamobile.features.presentation.home.viewmodel.HomeViewModel
import com.vpch.autoriamobile.features.presentation.splash.viewmodel.SplashViewModel
import io.ktor.client.HttpClient
import io.ktor.client.plugins.auth.Auth
import io.ktor.client.plugins.auth.providers.BearerTokens
import io.ktor.client.plugins.auth.providers.bearer
import io.ktor.client.plugins.contentnegotiation.ContentNegotiation
import io.ktor.serialization.kotlinx.json.json
import kotlinx.serialization.json.Json
import org.koin.core.module.dsl.viewModel
import org.koin.dsl.module


val sharedModule = module {
    single<Settings> { Settings() }
    single { TokenManager(settings = get()) }


    single {
        val tokenManager = get<TokenManager>()

        HttpClient {
            install(ContentNegotiation) {
                json(Json {
                    ignoreUnknownKeys = true
                    prettyPrint = true
                    isLenient = true
                })
            }

            install(Auth) {
                bearer {
                    loadTokens {
                        val accessToken = tokenManager.getAccessToken()
                        val refreshToken = tokenManager.getRefreshToken()

                        if (accessToken != null && refreshToken != null) {
                            BearerTokens(accessToken, refreshToken)
                        } else {
                            null
                        }
                    }

                    refreshTokens {
                        null
                    }
                }
            }
        }
    }


    single { AuthApiService(client = get()) }
    single { UserApiService(client = get()) }
    single<AuthRepository> { AuthRepositoryImpl(apiService = get()) }
    single<UserRepository> { UserRepositoryImpl(apiService = get()) }

    factory { RegisterUseCase(repository = get(), tokenManager = get()) }
    factory { LoginUseCase(repository = get(), tokenManager = get()) }
    factory { ObserveUserUseCase(userRepository = get()) }
    factory { LoadProfileUseCase(userRepository = get()) }
    factory { LogoutUseCase(tokenManager = get(), userRepository = get()) }

    viewModel { SplashViewModel(tokenManager = get(), loadProfileUseCase = get()) }
    viewModel { RegistrationViewModel(registerUseCase = get(), loadProfileUseCase = get()) }
    viewModel { LoginViewModel(loginUseCase = get(), loadProfileUseCase = get()) }
    viewModel { HomeViewModel( observeUserUseCase = get(), logoutUseCase = get())  }
}