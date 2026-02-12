package com.vpch.autoriamobile.app

import androidx.navigation3.runtime.NavKey
import kotlinx.serialization.Serializable

@Serializable
sealed interface Screen: NavKey{
    @Serializable
    data object Splash: Screen
    @Serializable
    data object Login: Screen
    @Serializable
    data object Registration: Screen
    @Serializable
    data object Home: Screen
}