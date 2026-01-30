package com.vpch.autoriamobile.core.presentation.theme

import androidx.compose.runtime.Composable


//@Composable
//fun LvivFixTheme(
//    darkTheme: Boolean = isSystemInDarkTheme(),
//    // Dynamic color is available on Android 12+
//    dynamicColor: Boolean = true,
//    content: @Composable () -> Unit
//) {
//    val customColors = if (darkTheme) DarkPalette else LightPalette
//
//    val colorScheme = when {
////        dynamicColor && Build.VERSION.SDK_INT >= Build.VERSION_CODES.S -> {
////            val context = LocalContext.current
////            if (darkTheme) dynamicDarkColorScheme(context) else dynamicLightColorScheme(context)
////        }
//
//        darkTheme -> DarkColorScheme
//        else -> LightColorScheme
//    }
//
//    CompositionLocalProvider(LocalAppColors provides customColors) {
//        MaterialTheme(
//            colorScheme = colorScheme,
//            typography = Typography,
//            content = content
//        )
//    }
//}

object AppTheme {
    val colors: AppColors
        @Composable
        get() = LocalAppColors.current

}