package com.vpch.autoriamobile.features.domain.auth.exceptions

class UserAlreadyExistsException : Exception()
class InvalidCredentialsException : Exception()
class ServerErrorException(message: String) : Exception(message)