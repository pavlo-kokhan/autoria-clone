package com.vpch.autoriamobile.features.domain.user.model

data class User(
    val email: String,
    val firstName: String,
    val lastName: String,
    val phone: String,
    val telegram: String
) {
    val fullName: String
        get() = if (firstName.isNotBlank() || lastName.isNotBlank()) "$firstName $lastName".trim() else email
}
