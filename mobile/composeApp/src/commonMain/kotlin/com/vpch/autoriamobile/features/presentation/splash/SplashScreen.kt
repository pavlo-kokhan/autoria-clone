package com.vpch.autoriamobile.features.presentation.splash

import androidx.compose.foundation.Image
import androidx.compose.foundation.background
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.material3.CircularProgressIndicator
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import com.vpch.autoriamobile.core.presentation.theme.AppTheme
import com.vpch.autoriamobile.features.presentation.splash.viewmodel.SplashEffect
import com.vpch.autoriamobile.features.presentation.splash.viewmodel.SplashViewModel
import org.koin.compose.viewmodel.koinViewModel

@Composable
fun SplashScreen(
    viewModel: SplashViewModel = koinViewModel(),
    onNavigateToHome: () -> Unit,
    onNavigateToLogin: () -> Unit
) {
    LaunchedEffect(viewModel) {
        viewModel.effect.collect { effect ->
            when(effect) {
                is SplashEffect.NavigateToHome -> onNavigateToHome()
                is SplashEffect.NavigateToLogin -> onNavigateToLogin()
            }
        }
    }

    Box(
        modifier = Modifier.fillMaxSize().background(AppTheme.colors.background),
        contentAlignment = Alignment.Center
    ) {
        CircularProgressIndicator(color = AppTheme.colors.background)
        //Image(painter = painterResource(Res.drawable.logo), ...)
    }
}