package com.vpch.autoriamobile

import android.app.Application
import com.vpch.autoriamobile.di.initKoin
import org.koin.android.ext.koin.androidContext

class AutoRiaApplication: Application() {

    override fun onCreate() {
        super.onCreate()
        initKoin {
            androidContext(this@AutoRiaApplication)
        }
    }
}