package com.vpch.autoriamobile.features.presentation.auth.components

import androidx.compose.foundation.border
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.foundation.text.KeyboardOptions
import androidx.compose.material3.Icon
import androidx.compose.material3.IconButton
import androidx.compose.material3.OutlinedTextField
import androidx.compose.material3.OutlinedTextFieldDefaults
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.focus.onFocusChanged
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.text.input.ImeAction
import androidx.compose.ui.text.input.KeyboardType
import androidx.compose.ui.text.input.PasswordVisualTransformation
import androidx.compose.ui.text.input.VisualTransformation
import androidx.compose.ui.unit.dp
import autoriamobile.composeapp.generated.resources.Res
import autoriamobile.composeapp.generated.resources.ic_clear
import autoriamobile.composeapp.generated.resources.ic_eye
import autoriamobile.composeapp.generated.resources.ic_eye_closed
import com.vpch.autoriamobile.core.presentation.theme.AppTheme
import com.vpch.autoriamobile.features.presentation.components.text.CustomText
import org.jetbrains.compose.resources.vectorResource

@Composable
fun AuthTextField(
    value: String,
    onValueChange: (String) -> Unit,
    placeholder: String,
    modifier: Modifier = Modifier,
    errorMessage: String? = null,
    isPassword: Boolean = false,
    keyboardType: KeyboardType = KeyboardType.Text,
    imeAction: ImeAction = ImeAction.Next
) {
    var isPasswordVisible by remember { mutableStateOf(false) }
    var isFocused by remember { mutableStateOf(false) }

    val borderColor = when {
        errorMessage != null -> AppTheme.colors.textError
        isFocused -> AppTheme.colors.buttonPrimary
        else -> AppTheme.colors.textPrimary.copy(alpha = 0.5f)
    }

    val borderWidth = if (isFocused) 1.dp else 0.5.dp

    Column(modifier = modifier) {
        OutlinedTextField(
            value = value,
            onValueChange = onValueChange,
            placeholder = {
                CustomText(
                    text = placeholder,
                    color = AppTheme.colors.textPrimary.copy(alpha = 0.5f),
                    style = AppTheme.typography.bodyMedium
                )
            },
            modifier = Modifier
                .fillMaxWidth()
                .onFocusChanged { isFocused = it.isFocused }
                .border(
                    width = borderWidth,
                    color = borderColor,
                    shape = RoundedCornerShape(12.dp)
                ),
            textStyle = AppTheme.typography.bodyMedium.copy(color = AppTheme.colors.textPrimary),
            shape = RoundedCornerShape(12.dp),
            isError = errorMessage != null,
            singleLine = true,
            colors = OutlinedTextFieldDefaults.colors(
                focusedBorderColor = Color.Transparent,
                unfocusedBorderColor = Color.Transparent,
                errorBorderColor = Color.Transparent,
                cursorColor = AppTheme.colors.buttonPrimary,
                focusedContainerColor = Color.Transparent,
                unfocusedContainerColor = Color.Transparent
            ),
            visualTransformation = if (isPassword && !isPasswordVisible) {
                PasswordVisualTransformation()
            } else {
                VisualTransformation.None
            },
            keyboardOptions = KeyboardOptions(
                keyboardType = keyboardType,
                imeAction = imeAction
            ),
            trailingIcon = {
                if (isPassword) {
                    val iconRes = if (isPasswordVisible) Res.drawable.ic_eye else Res.drawable.ic_eye_closed
                    IconButton(onClick = { isPasswordVisible = !isPasswordVisible }) {
                        Icon(
                            imageVector = vectorResource(iconRes),
                            contentDescription = "Show/Hide Password",
                            tint = AppTheme.colors.iconPrimary
                        )
                    }
                } else if (value.isNotEmpty()) {
                    IconButton(onClick = { onValueChange("") }) {
                        Icon(
                            imageVector = vectorResource(Res.drawable.ic_clear),
                            contentDescription = "Clear text",
                            tint = AppTheme.colors.iconSecondary
                        )
                    }
                }
            }
        )

        CustomText(
            text = errorMessage ?: " ",
            color = if (errorMessage != null) AppTheme.colors.textError else Color.Transparent,
            style = AppTheme.typography.bodySmall,
            modifier = Modifier.padding(start = 8.dp, top = 4.dp)
        )
    }
}