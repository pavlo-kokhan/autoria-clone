package com.vpch.autoriamobile.app

import androidx.compose.material3.MaterialTheme
import androidx.compose.runtime.Composable
import androidx.compose.ui.tooling.preview.Preview
import com.vpch.autoriamobile.features.presentation.auth.login.LoginScreen


@Composable
@Preview
fun App() {
    MaterialTheme {
        NavigationRoot()
    }
}