package com.vpch.autoriamobile.features.presentation.components.text

import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.text.TextStyle
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.style.TextDecoration
import com.vpch.autoriamobile.core.presentation.theme.AppTheme

@Composable
fun CustomText(
    isTitle: Boolean = false,
    text: String,
    style: TextStyle = if (isTitle) AppTheme.typography.titleMedium else AppTheme.typography.bodyMedium,
    color: Color = AppTheme.colors.textPrimary,
    fontWeight: FontWeight = FontWeight.Normal,
    textDecoration: TextDecoration = TextDecoration.None,
    modifier: Modifier = Modifier
    ) {
    Text(
        text = text,
        style = style,
        color = color,
        fontWeight = fontWeight,
        textDecoration = textDecoration,
        modifier = modifier
    )
}