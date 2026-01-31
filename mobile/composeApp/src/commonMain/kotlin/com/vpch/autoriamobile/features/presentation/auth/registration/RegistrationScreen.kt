package com.vpch.autoriamobile.features.presentation.auth.registration

import androidx.compose.foundation.background
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxHeight
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.Button
import androidx.compose.material3.ButtonDefaults
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Snackbar
import androidx.compose.material3.SnackbarHost
import androidx.compose.material3.SnackbarHostState
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.getValue
import androidx.compose.runtime.remember
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.input.ImeAction
import androidx.compose.ui.text.input.KeyboardType
import androidx.compose.ui.text.style.TextDecoration
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import autoriamobile.composeapp.generated.resources.Res
import autoriamobile.composeapp.generated.resources.btn_signin
import autoriamobile.composeapp.generated.resources.btn_signup
import autoriamobile.composeapp.generated.resources.email_placeholder
import autoriamobile.composeapp.generated.resources.have_profile
import autoriamobile.composeapp.generated.resources.password_placeholder
import autoriamobile.composeapp.generated.resources.registration_title
import com.vpch.autoriamobile.core.presentation.theme.AppTheme
import com.vpch.autoriamobile.features.presentation.auth.components.AuthTextField
import com.vpch.autoriamobile.features.presentation.components.text.CustomText
import org.jetbrains.compose.resources.stringResource
import org.koin.compose.viewmodel.koinViewModel

@Composable
@Preview(showBackground = true)
fun RegistrationScreen(
    modifier: Modifier = Modifier,
    viewModel: RegistrationViewModel = koinViewModel(),
    onNavigateToLogin: () -> Unit,
    onNavigateToHome: () -> Unit
) {
    val state by viewModel.state.collectAsState()
    val snackbarHostState = remember { SnackbarHostState() }

    val errorMessage = state.errorRes?.let { stringResource(it) }

    LaunchedEffect(errorMessage) {
        if (errorMessage != null) {
            snackbarHostState.showSnackbar(
                message = errorMessage,
                withDismissAction = true
            )
        }
    }
    LaunchedEffect(viewModel) {
        viewModel.effect.collect { effect ->
            when (effect) {
                is RegistrationEffect.NavigateToHome -> onNavigateToHome()
                is RegistrationEffect.NavigateToLogin -> onNavigateToLogin()
            }
        }
    }

    Scaffold(
        snackbarHost = {
            SnackbarHost(hostState = snackbarHostState) { data ->
                Snackbar(
                    snackbarData = data,
                    containerColor = AppTheme.colors.textError,
                    contentColor = Color.White
                )
            }
        }
    ) { paddingValues ->
        Column(
            modifier = modifier
                .fillMaxSize()
                .background(color = AppTheme.colors.background)
                .padding(paddingValues)
                .padding(horizontal = 20.dp),
            horizontalAlignment = Alignment.CenterHorizontally,
        ) {
            Spacer(modifier = Modifier.fillMaxHeight(0.2f))
            CustomText(
                text = stringResource(Res.string.registration_title),
                isTitle = true,
                style = AppTheme.typography.titleLarge,
                fontWeight = FontWeight.Bold,
            )
            Spacer(modifier = Modifier.height(32.dp))
            AuthTextField(
                value = state.email,
                onValueChange = {
                    viewModel.onEvent(RegistrationEvent.OnEmailChange(it))
                },
                placeholder = stringResource(Res.string.email_placeholder),
                errorMessage = state.emailError,
                keyboardType = KeyboardType.Email,
                imeAction = ImeAction.Next
            )
            Spacer(modifier = Modifier.height(4.dp))
            AuthTextField(
                value = state.password,
                onValueChange = { viewModel.onEvent(RegistrationEvent.OnPasswordChange(it)) },
                placeholder = stringResource(Res.string.password_placeholder),
                isPassword = true,
                errorMessage = state.passwordError,
                keyboardType = KeyboardType.Password,
                imeAction = ImeAction.Done,
            )
            Spacer(modifier = Modifier.height(12.dp))
            Button(
                onClick = { viewModel.onEvent(RegistrationEvent.OnRegisterClick) },
                enabled = !state.isLoading,
                modifier = Modifier.fillMaxWidth()
                    .height(48.dp),
                colors = ButtonDefaults.buttonColors(
                    containerColor = AppTheme.colors.buttonPrimary,
                    contentColor = AppTheme.colors.textSecondary
                )
            ) {
                CustomText(
                    text = stringResource(Res.string.btn_signup),
                    color = AppTheme.colors.textSecondary
                )
            }
            Spacer(modifier = Modifier.height(20.dp))
            CustomText(
                text = stringResource(Res.string.have_profile),
            )
            Spacer(modifier = Modifier.height(4.dp))
            CustomText(
                text = stringResource(Res.string.btn_signin),
                fontWeight = FontWeight.Bold,
                textDecoration = TextDecoration.Underline,
                modifier = Modifier.clickable(
                    onClick = { viewModel.onEvent(RegistrationEvent.OnLoginClick) }
                )
            )

        }
    }
}
