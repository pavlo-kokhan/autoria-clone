package com.vpch.autoriamobile.core.domain.validation

object AuthSpecs {
    const val PASSWORD_MIN_LENGTH = 8
    const val EMAIL_MAX_LENGTH = 32

    fun isEmailValid(email: String): Boolean {
        return email.isNotBlank() &&
                email.contains("@") &&
                email.length <= EMAIL_MAX_LENGTH
    }

    fun isPasswordValid(password: String): Boolean {
        return password.length >= PASSWORD_MIN_LENGTH
    }
}