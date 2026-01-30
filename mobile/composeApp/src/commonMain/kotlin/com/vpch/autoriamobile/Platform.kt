package com.vpch.autoriamobile

interface Platform {
    val name: String
}

expect fun getPlatform(): Platform