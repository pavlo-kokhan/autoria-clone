package com.vpch.autoriamobile.core.presentation.theme


import androidx.compose.runtime.Composable
import androidx.compose.ui.text.TextStyle
import androidx.compose.ui.text.font.FontFamily
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.unit.sp
import autoriamobile.composeapp.generated.resources.Res
import autoriamobile.composeapp.generated.resources.stacksansheadline_bold
import autoriamobile.composeapp.generated.resources.stacksansheadline_medium
import autoriamobile.composeapp.generated.resources.stacksansheadline_regular
import autoriamobile.composeapp.generated.resources.stacksansheadline_semibold
import autoriamobile.composeapp.generated.resources.stacksanstext_bold
import autoriamobile.composeapp.generated.resources.stacksanstext_medium
import autoriamobile.composeapp.generated.resources.stacksanstext_regular
import autoriamobile.composeapp.generated.resources.stacksanstext_semibold
import org.jetbrains.compose.resources.Font
import androidx.compose.material3.Typography

@Composable
fun stackSansHeadlineFamily() = FontFamily(
    Font(Res.font.stacksansheadline_medium, FontWeight.Medium),
    Font(Res.font.stacksansheadline_regular, FontWeight.Normal),
    Font(Res.font.stacksansheadline_bold, FontWeight.Bold),
    Font(Res.font.stacksansheadline_semibold, FontWeight.SemiBold)
)

@Composable
fun stackSansTextFamily() = FontFamily(
    Font(Res.font.stacksanstext_medium, FontWeight.Medium),
    Font(Res.font.stacksanstext_regular, FontWeight.Normal),
    Font(Res.font.stacksanstext_bold, FontWeight.Bold),
    Font(Res.font.stacksanstext_semibold, FontWeight.SemiBold)
)

@Composable
fun AppTypography(): Typography {
    val stackSansHeadline = stackSansHeadlineFamily()
    val stackSansText = stackSansTextFamily()

    return Typography(
        displayLarge = TextStyle(
            fontFamily = stackSansHeadline,
            fontWeight = FontWeight.Bold,
            fontSize = 30.sp
        ),
        titleLarge = TextStyle(
            fontFamily = stackSansHeadline,
            fontWeight = FontWeight.Medium,
            fontSize = 24.sp
        ),
        titleMedium = TextStyle(
            fontFamily = stackSansHeadline,
            fontWeight = FontWeight.Normal,
            fontSize = 16.sp
        ),
        titleSmall = TextStyle(
            fontFamily = stackSansHeadline,
            fontWeight = FontWeight.Normal,
            fontSize = 14.sp
        ),
        bodyLarge = TextStyle(
            fontFamily = stackSansText,
            fontWeight = FontWeight.Medium,
            fontSize = 24.sp,
        ),
        bodyMedium = TextStyle(
            fontFamily = stackSansText,
            fontWeight = FontWeight.Medium,
            fontSize = 16.sp,
        ),
        bodySmall = TextStyle(
            fontFamily = stackSansText,
            fontWeight = FontWeight.Medium,
            fontSize = 14.sp,
        )
    )
}