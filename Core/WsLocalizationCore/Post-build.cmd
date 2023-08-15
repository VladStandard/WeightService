@echo off
cls
setlocal
echo ------------------------------------------------------------
echo ---          WsLocalizationCore/Post-build.cmd           ---
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
exit %ErrorLevel%

:isConnectGood <host>
xcopy "Locales\DeviceControl.loc.json" "\\palych\Install\VSSoft\Locales\" /Y /S /Q /F /R /V >nul
xcopy "Locales\LabelPrint.loc.json" "\\palych\Install\VSSoft\Locales\" /Y /S /Q /F /R /V >nul
xcopy "Locales\Tests.loc.json" "\\palych\Install\VSSoft\Locales\" /Y /S /Q /F /R /V >nul
echo [v] The files "Locales\*.json" has been successfully copied.
goto :end

:isConnectBad <host>
echo [x] The host [%~1] is offline

:end
echo [ ] End job
endlocal
exit 0
