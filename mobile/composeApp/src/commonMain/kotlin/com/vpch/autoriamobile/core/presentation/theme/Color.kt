package com.vpch.autoriamobile.core.presentation.theme

import androidx.compose.runtime.staticCompositionLocalOf
import androidx.compose.ui.graphics.Color

val Blue40 = Color(0xFF1C4D8D)
val Grey40 = Color(0xFF454444)
val Grey80 = Color(0xFFAAA7A7)
val White = Color(0xFFFFFFFF)
val ErrorRed = Color(0xFFDF2A2A)
val Black = Color(0xFF000000)


data class AppColors(
    val background: Color,
    val buttonPrimary: Color,
    val buttonSecondary: Color,
    val strokePrimary: Color,
    val iconPrimary: Color,
    val iconSecondary: Color,
    val textPrimary: Color,
    val textSecondary: Color,
    val textError: Color,
)

val LightPalette = AppColors(
    background = White,
    buttonPrimary = Blue40,
    buttonSecondary = Grey40,
    strokePrimary = Grey80,
    iconPrimary = Black,
    iconSecondary = Grey80,
    textPrimary = Black,
    textSecondary = White,
    textError = ErrorRed,
)


//val DarkPalette = AppColors(
//    background = Black,
//    buttonPrimary = Blue40,
//    buttonSecondary = Grey40,
//    strokePrimary = Grey80,
//    iconPrimary = White,
//    iconSecondary = Grey80,
//    textPrimary = White,
//    textSecondary = Grey80,
//    textError = ErrorRed
//)


val LocalAppColors = staticCompositionLocalOf { LightPalette }