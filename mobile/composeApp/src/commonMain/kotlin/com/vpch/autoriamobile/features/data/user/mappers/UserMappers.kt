package com.vpch.autoriamobile.features.data.user.mappers

import com.vpch.autoriamobile.features.data.user.dto.UserResponseDto
import com.vpch.autoriamobile.features.domain.user.model.User

fun UserResponseDto.toDomain(): User = User(
    email = email,
    firstName = firstName ?: "",
    lastName = lastName ?: "",
    phone = phoneNumber ?: "",
    telegram = telegramUserName ?: ""
)