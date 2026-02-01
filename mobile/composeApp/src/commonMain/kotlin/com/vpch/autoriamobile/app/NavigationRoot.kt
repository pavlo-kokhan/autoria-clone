package com.vpch.autoriamobile.app

import androidx.compose.runtime.Composable
import androidx.compose.runtime.mutableStateListOf
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.navigation3.runtime.entryProvider
import androidx.navigation3.ui.NavDisplay
import com.vpch.autoriamobile.features.presentation.auth.login.LoginScreen
import com.vpch.autoriamobile.features.presentation.auth.registration.RegistrationScreen
import com.vpch.autoriamobile.features.presentation.home.HomeScreen

@Composable
fun NavigationRoot() {
    val backStack = remember { mutableStateListOf<Screen>(Screen.Login) }

    NavDisplay(
        backStack = backStack,
        onBack = { backStack.removeLastOrNull() },
        entryProvider = entryProvider {
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
                HomeScreen()
            }
        }
    )
}
