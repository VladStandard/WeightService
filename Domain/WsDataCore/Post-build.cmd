@echo off
cls
setlocal
echo ------------------------------------------------------------
echo ---              WsDataCore/Post-build.cmd               ---
echo ------------------------------------------------------------
set "host=palych"
call :job "%host%"
goto :end

:job <host>
echo [ ] Start job
echo [ ] Check the connection to the host [%~1]
call :ping "%~1" && call :isConnectGood "%~1" || call :isConnectBad "%~1"
echo ------------------------------------------------------------
exit %ErrorLevel%

:ping <host>
ping "%~1" -n 1 | find "TTL=" >nul

:isConnectGood <host>
xcopy "appsettings.json" "\\palych\Install\VSSoft\appsettings\" /Y /S /Q /F /R /V >nul
xcopy "appsettings.DevelopVS.json" "\\palych\Install\VSSoft\appsettings\" /Y /S /Q /F /R /V >nul
xcopy "appsettings.ReleaseVS.json" "\\palych\Install\VSSoft\appsettings\" /Y /S /Q /F /R /V >nul
echo [v] The files "appsettings*.json" has been successfully copied.
xcopy "machine.config" "\\palych\Install\VSSoft\appsettings\" /Y /S /Q /F /R /V >nul
echo [v] The files "machine.config" has been successfully copied.
goto :end

:isConnectBad <host>
echo [x] The host [%~1] is offline

:end
echo [ ] End job
endlocal
exit 0
