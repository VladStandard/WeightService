@echo off
dotnet publish ..\ScalesVS.csproj --configuration ReleaseVS --framework net7.0-windows10.0.19041.0 --runtime win10-x64 /p:AppxPackageDir=\\palych\Install\Apps\ScalesVs\ /p:AppInstallerUri=\\palych\Install\Apps\ScalesVs
