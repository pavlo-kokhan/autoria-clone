package com.vpch.autoriamobile.core.domain.validation

import autoriamobile.composeapp.generated.resources.Res
import autoriamobile.composeapp.generated.resources.error_incorrect_email
import autoriamobile.composeapp.generated.resources.error_short_password
import com.vpch.autoriamobile.Constants
import org.jetbrains.compose.resources.StringResource

object AuthValidator {

    fun validateEmail(email: String): StringResource? {
        return if (!AuthSpecs.isEmailValid(email)) {
            Res.string.error_incorrect_email
        } else {
            null
        }
    }

    fun validatePassword(password: String): StringResource? {
        return if (!AuthSpecs.isPasswordValid(password)) {
            Res.string.error_short_password
        } else {
            null
        }
    }
}