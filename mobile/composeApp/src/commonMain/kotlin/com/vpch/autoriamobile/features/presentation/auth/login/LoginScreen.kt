package com.vpch.autoriamobile.features.presentation.auth.login

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
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.input.ImeAction
import androidx.compose.ui.text.input.KeyboardType
import androidx.compose.ui.text.style.TextDecoration
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import autoriamobile.composeapp.generated.resources.Res
import autoriamobile.composeapp.generated.resources.btn_signin
import autoriamobile.composeapp.generated.resources.btn_signup
import autoriamobile.composeapp.generated.resources.dont_have_profile
import autoriamobile.composeapp.generated.resources.email_placeholder
import autoriamobile.composeapp.generated.resources.login_title
import autoriamobile.composeapp.generated.resources.password_placeholder
import com.vpch.autoriamobile.core.presentation.theme.AppTheme
import com.vpch.autoriamobile.features.presentation.auth.components.AuthTextField
import com.vpch.autoriamobile.features.presentation.components.text.CustomText
import org.jetbrains.compose.resources.stringResource

@Composable
@Preview(showBackground = true)
fun LoginScreen(
    modifier: Modifier = Modifier,
) {
    var email by remember { mutableStateOf("") }
    var password by remember { mutableStateOf("") }
    var emailError by remember { mutableStateOf<String?>(null) }
    Column(
        modifier = modifier
            .fillMaxSize()
            .background(color = AppTheme.colors.background)
            .padding(horizontal = 20.dp),
        horizontalAlignment = Alignment.CenterHorizontally,
    ) {
        Spacer(modifier = Modifier.fillMaxHeight(0.2f))
        CustomText(
            text = stringResource(Res.string.login_title),
            isTitle = true,
            style = AppTheme.typography.titleLarge,
            fontWeight = FontWeight.Bold,
        )
        Spacer(modifier = Modifier.height(32.dp))
        AuthTextField(
            value = email,
            onValueChange = {
                email = it
                emailError = null
            },
            placeholder = stringResource(Res.string.email_placeholder),
            errorMessage = emailError,
            keyboardType = KeyboardType.Email,
            imeAction = ImeAction.Next
        )
        Spacer(modifier = Modifier.height(4.dp))
        AuthTextField(
            value = password,
            onValueChange = { password = it },
            placeholder = stringResource(Res.string.password_placeholder),
            isPassword = true,
            keyboardType = KeyboardType.Password,
            imeAction = ImeAction.Done,
        )
        Spacer(modifier = Modifier.height(12.dp))
        Button(
            onClick = {},
            modifier = Modifier.fillMaxWidth()
                .height(48.dp),
            colors = ButtonDefaults.buttonColors(
                containerColor = AppTheme.colors.buttonPrimary,
                contentColor = AppTheme.colors.textSecondary
            )
        ) {
            CustomText(
                text = stringResource(Res.string.btn_signin),
                color = AppTheme.colors.textSecondary
            )
        }
        Spacer(modifier = Modifier.height(20.dp))
        CustomText(
            text = stringResource(Res.string.dont_have_profile),
        )
        Spacer(modifier = Modifier.height(4.dp))
        CustomText(
            text = stringResource(Res.string.btn_signup),
            fontWeight = FontWeight.Bold,
            textDecoration = TextDecoration.Underline,
            modifier = Modifier.clickable(
                onClick = {}
            )
        )

    }
}
