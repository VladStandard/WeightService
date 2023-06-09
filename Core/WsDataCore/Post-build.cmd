@echo off
cls
setlocal
echo ------------------------------------------------------------
echo ---              WsDataCore/Post-build.cmd               ---
echo ------------------------------------------------------------

set "host=palych"
call :job "%host%"
endlocal
exit /b 0

:job <host>
echo --- [ ] Check the connection to the host [%~1] ---
call :ping "%~1" && call :isConnectGood "%~1" || call :isConnectBad "%~1"
echo ------------------------------------------------------------
exit /b %ErrorLevel%

:ping <host>
echo --- [ ] The host %~1 is ping ---
ping "%~1" -n 1 | find "TTL=" >nul
exit /b %ErrorLevel%

:isConnectGood <host>
echo --- [v] The host [%~1] is online ---
xcopy "appsettings.DevelopAleksandrov.json" "\\palych\Install\VSSoft\appsettings\" /Y /S /Q /F /R /V >nul
xcopy "appsettings.DevelopMorozov.json" "\\palych\Install\VSSoft\appsettings\" /Y /S /Q /F /R /V >nul
xcopy "appsettings.DevelopVS.json" "\\palych\Install\VSSoft\appsettings\" /Y /S /Q /F /R /V >nul
echo --- [v] xcopy "appsettings.Develop*.json" is complete ---
xcopy "appsettings.ReleaseAleksandrov.json" "\\palych\Install\VSSoft\appsettings\" /Y /S /Q /F /R /V >nul
xcopy "appsettings.ReleaseMorozov.json" "\\palych\Install\VSSoft\appsettings\" /Y /S /Q /F /R /V >nul
xcopy "appsettings.ReleaseVS.json" "\\palych\Install\VSSoft\appsettings\" /Y /S /Q /F /R /V >nul
echo --- [v] xcopy "appsettings.Release*.json" is complete ---

:isConnectBad <host>
echo --- [x] The host [%~1] is offline ---
