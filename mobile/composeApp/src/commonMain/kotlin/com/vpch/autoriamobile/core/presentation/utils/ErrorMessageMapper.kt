package com.vpch.autoriamobile.core.presentation.utils

import autoriamobile.composeapp.generated.resources.Res
import autoriamobile.composeapp.generated.resources.error_no_internet
import autoriamobile.composeapp.generated.resources.error_unknown
import autoriamobile.composeapp.generated.resources.error_user_exists
import com.vpch.autoriamobile.features.domain.auth.exceptions.ServerErrorException
import com.vpch.autoriamobile.features.domain.auth.exceptions.UserAlreadyExistsException
import org.jetbrains.compose.resources.StringResource

fun Throwable.toUiErrorMessage(): StringResource {
    return when (this) {
        is UserAlreadyExistsException -> Res.string.error_user_exists
        else -> Res.string.error_unknown
    }
}