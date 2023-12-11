@echo off
dotnet publish ..\ScalesHybrid.csproj --configuration ReleaseVS --framework net7.0-windows10.0.19041.0 --runtime win10-x64 /p:AppxPackageDir=\\palych\Install\VSSoft\ScalesVs\ /p:AppInstallerUri=\\palych\Install\VSSoft\ScalesVs\
