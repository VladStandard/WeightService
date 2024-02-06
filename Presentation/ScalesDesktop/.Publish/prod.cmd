@echo off
dotnet publish ..\ScalesDesktop.csproj --configuration ReleaseVS --framework net8.0-windows10.0.19041.0 --runtime win10-x64 /p:AppxPackageDir=\\palych\Install\Apps\ScalesVs\ /p:AppInstallerUri=\\palych\Install\Apps\ScalesVs && powershell -File "Utils\UpdateAppInstaller.ps1" -appInstallerFilePath "\\palych\Install\Apps\ScalesVs\ScalesDesktop_x64.appinstaller"
