@echo off
cls
setlocal
echo ------------------------------------------------------------
echo ---              WsDataCore/Post-build.cmd               ---
echo ------------------------------------------------------------
set "host=palych"
call :job "%host%"
goto :end

:job
echo [ ] Start job
echo [ ] Check the connection to the host [%~1]
call :ping "%~1" && call :isConnectGood "%~1" || call :isConnectBad "%~1"
echo ------------------------------------------------------------
exit %ErrorLevel%

:ping <host>
ping "%~1" -n 1 | find "TTL=" >nul
exit /b %ErrorLevel%

:isConnectGood <host>
xcopy "appsettings.DevelopAleksandrov.json" "\\palych\Install\VSSoft\appsettings\" /Y /S /Q /F /R /V >nul
xcopy "appsettings.DevelopMorozov.json" "\\palych\Install\VSSoft\appsettings\" /Y /S /Q /F /R /V >nul
xcopy "appsettings.DevelopVS.json" "\\palych\Install\VSSoft\appsettings\" /Y /S /Q /F /R /V >nul
echo [v] The files "appsettings.Develop*.json" has been successfully copied.
xcopy "appsettings.ReleaseAleksandrov.json" "\\palych\Install\VSSoft\appsettings\" /Y /S /Q /F /R /V >nul
xcopy "appsettings.ReleaseMorozov.json" "\\palych\Install\VSSoft\appsettings\" /Y /S /Q /F /R /V >nul
xcopy "appsettings.ReleaseVS.json" "\\palych\Install\VSSoft\appsettings\" /Y /S /Q /F /R /V >nul
echo [v] The files "appsettings.Release*.json" has been successfully copied.
goto :end

:isConnectBad <host>
echo [x] The host [%~1] is offline

:end
echo [ ] End job
endlocal
exit 0
