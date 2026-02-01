package com.vpch.autoriamobile.features.presentation.home

import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.Button
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.getValue
import androidx.compose.ui.Modifier
import com.vpch.autoriamobile.features.presentation.components.text.CustomText
import com.vpch.autoriamobile.features.presentation.home.viewmodel.HomeEffect
import com.vpch.autoriamobile.features.presentation.home.viewmodel.HomeEvent
import com.vpch.autoriamobile.features.presentation.home.viewmodel.HomeViewModel
import org.koin.compose.viewmodel.koinViewModel

@Composable
fun HomeScreen(
    modifier: Modifier = Modifier,
    viewModel: HomeViewModel = koinViewModel(),
    onNavigateToLogin: () -> Unit
) {
    val user by viewModel.user.collectAsState()

    LaunchedEffect(viewModel) {
        viewModel.effect.collect { effect ->
            when (effect) {
                is HomeEffect.NavigateToLogin -> onNavigateToLogin()
            }
        }
    }

    Scaffold { innerPadding ->
        Column(
            modifier = modifier.fillMaxSize()
                .padding(innerPadding)
        ){
            Text(
                text = user?.email ?: "Don't have email"
            )
            Button(
                onClick = {
                    viewModel.onEvent(HomeEvent.OnLogoutClick)
                }
            ) {
                CustomText(text = "Вийти")
            }
        }
    }

}
