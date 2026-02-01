package com.vpch.autoriamobile.app

import androidx.compose.runtime.Composable
import androidx.compose.runtime.mutableStateListOf
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.navigation3.runtime.entryProvider
import androidx.navigation3.ui.NavDisplay
import com.vpch.autoriamobile.core.data.local.TokenManager
import com.vpch.autoriamobile.features.presentation.auth.login.LoginScreen
import com.vpch.autoriamobile.features.presentation.auth.registration.RegistrationScreen
import com.vpch.autoriamobile.features.presentation.home.HomeScreen
import com.vpch.autoriamobile.features.presentation.splash.SplashScreen
import org.koin.compose.koinInject

@Composable
fun NavigationRoot() {
    val tokenManager: TokenManager = koinInject()
    val startScreen = if (tokenManager.isUserLoggedIn()) {
        Screen.Home
    } else {
        Screen.Login
    }

    val backStack = remember { mutableStateListOf<Screen>(Screen.Splash) }

    NavDisplay(
        backStack = backStack,
        onBack = { backStack.removeLastOrNull() },
        entryProvider = entryProvider {
            entry<Screen.Splash> {
                SplashScreen(
                    onNavigateToHome = {
                        backStack.clear()
                        backStack.add(Screen.Home)
                    },
                    onNavigateToLogin = {
                        backStack.clear()
                        backStack.add(Screen.Login)
                    }
                )
            }
            entry<Screen.Login> {
                LoginScreen(
                    onNavigateToRegistration = {
                        backStack.clear()
                        backStack.add(Screen.Registration)
                    },
                    onNavigateToHome = {
                        backStack.clear()
                        backStack.add(Screen.Home)
                    }
                )
            }
            entry<Screen.Registration> {
                RegistrationScreen(
                    onNavigateToLogin = {
                        backStack.clear()
                        backStack.add(Screen.Login)
                    },
                    onNavigateToHome = {
                        backStack.clear()
                        backStack.add(Screen.Home)
                    }
                )
            }
            entry<Screen.Home> {
                HomeScreen(
                    onNavigateToLogin = {
                        backStack.clear()
                        backStack.add(Screen.Login)
                    }
                )
            }
        }
    )
}
