package com.vpch.autoriamobile.features.domain.auth.exceptions

class UserAlreadyExistsException : Exception()
class ServerErrorException(message: String) : Exception(message)