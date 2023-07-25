# Publish resources

## Шаблон рассылки обновления
Коллеги, произведено обновление весовой платформы.
- АРМ фасовщика "Печать этикеток" v.0.x.xxx.
- Веб-приложение "Управление устройствами" v.0.x.xxx доступно по ссылке: https://device-control.kolbasa-vs.local/
Замечания и пожелания приветствуются, слать можно на почту (morozov_dv@kolbasa-vs.ru), либо в Discord (DamianMorozov#1034).

## Ссылки
Предварительная версия веб-приложения для тестирования  https://device-control-dev-preview.kolbasa-vs.local/
Стабильная версия веб-приложения для тестирования		https://device-control-dev.kolbasa-vs.local/
Предварительная версия веб-приложения для работы		https://device-control-prod-preview.kolbasa-vs.local/
Стабильная версия веб-приложения для работы				https://device-control.kolbasa-vs.local/

## ScalesUI
  DevelopVS: switch to DevelopVS
    Folder:  \\palych\Install\VSSoft\Scales-2-Preview\
    SQL:     CREATIO\INS1 + SCALES
  ReleaseVS: switch to ReleaseVS
    Folder:  \\palych\Install\VSSoft\Scales-3-Release\
    SQL:     PALYCH\LUTON + ScalesDB

## Для установки ПО `Печать этикеток` на сегодня (`2023-04-12`) требуется установить:
.NET Framework 4.7.2 Offline Installer: `\\palych\Install\NET\ndp472-kb4054530-x86-x64-allos-enu.exe`
.NET Framework 4.7.2 Offline RUS Language Pack: `\\palych\Install\NET\ndp472-kb4054530-x86-x64-allos-rus.exe`
.NET Framework 4.8.1 Offline Installer: `\\palych\Install\NET\ndp481-x86-x64-allos-enu.exe`
.NET Framework 4.8.1 Offline RUS Language Pack: `\\palych\Install\NET\ndp481-x86-x64-allos-rus.exe`

## WsWebApiScales
  DevelopVS: switch to DevelopVS
    SCALES-DEV-PREVIEW
        Server: IIS-DEV
        Site path: WEB-API-SCALES-DEV-PREVIEW
            ✓ Passive mode
        User name: <userName>
        Password: <password>
        Destination URL
        Configuration: DevelopVS - x64
        Target Framework: net7.0
        Deployment Mode: Framework-dependent
        Target Runtime: Portable
        File Publish Options
            ✓ Delete all existing files prior to publish
        Databases DefaultConnection
            ✓ Use this connection string at runtime
            Server=CREATIO\INS1;Database=SCALES;Uid=scale01;Password=scale01;Timeout=900;TrustServerCertificate=true;
    SCALES-DEV
        Server: IIS-DEV
        Site path: WEB-API-SCALES-DEV
            ✓ Passive mode
        User name: <userName>
        Password: <password>
        Destination URL
        Configuration: DevelopVS - x64
        Target Framework: net7.0
        Deployment Mode: Framework-dependent
        Target Runtime: Portable
        File Publish Options
            ✓ Delete all existing files prior to publish
        Databases DefaultConnection
            ✓ Use this connection string at runtime
            Server=CREATIO\INS1;Database=SCALES;Uid=scale01;Password=scale01;Timeout=900;TrustServerCertificate=true;
  ReleaseVS: switch to ReleaseVS
    Server: IIS-DEV
    Site path: WEB-API-SCALES-DEV-PREVIEW
        ✓ Passive mode
    User name: <userName>
    Password: <password>
    Destination URL
    Configuration: DevelopVS - x64
    Target Framework: net7.0
    Deployment Mode: Framework-dependent
    Target Runtime: Portable
    File Publish Options
        ✓ Delete all existing files prior to publish
    Databases DefaultConnection
        ✓ Use this connection string at runtime
        Server=CREATIO\INS1;Database=SCALES;Uid=scale01;Password=scale01;Timeout=900;TrustServerCertificate=true;

## Hyper-V
dhvsm01 (10.0.24.248)

## DeviceControl
  Debug:     
             IIS-DEV (10.0.204.17): https://device-control-dev.kolbasa-vs.local/
             IIS-DEV (10.0.204.17): https://device-control-dev-preview.kolbasa-vs.local/
    SQL:     CREATIO\INS1 + SCALES
    DB:      SCALES
    RDP:     mstsc /v:iis-dev
    CONFIG:  appsettings.Develop*.json -> appsettings.json
    IIS:     creatio:5009 + device-test
  Release:   PALYCH (10.0.204.24):  https://device-control.kolbasa-vs.local/
             PALYCH (10.0.204.24):  https://device-control-prod.kolbasa-vs.local/
             PALYCH (10.0.204.24):  https://device-control-prod-preview.kolbasa-vs.local/
    SQL:     PALYCH\LUTON
    DB:      ScalesDB
    RDP:     mstsc /v:palych
    CONFIG:  appsettings.Release*.json -> appsettings.json
    IIS:     palych + device-control

## BlazorResourcesVs
  Debug:     https://resources-test.kolbasa-vs.local/
    SQL:     
    DB:      
    RDP:     mstsc /v:creatio
    CONFIG:  appsettings.Develop*.json -> appsettings.json
    IIS:     creatio + resources-test
  Release:   https://resources-vs.kolbasa-vs.local/
    SQL:     PALYCH\LUTON
    DB:      ScalesDB
    RDP:     mstsc /v:palych
    CONFIG:  appsettings.Release*.json -> appsettings.json
    IIS:     palych + resources-vs

## Terra T1000 | WebApiTerra1000
  Debug => T1000-DEV & T1000-DEV-PREVIEW:
    https://t1000-dev.kolbasa-vs.local:443/
    https://t1000-dev-preview.kolbasa-vs.local:443/
    RDP:     mstsc /v:IIS-PROD
    CONFIG:  appsettings.Development.json -> appsettings.json
    SQL:     CREATIO\INS1 + VSDWH
  Release:   t1000:221
    https://t1000.kolbasa-vs.local:443/
    https://t1000-preview.kolbasa-vs.local:443/
    RDP:     mstsc /v:IIS-PROD
    CONFIG:  appsettings.json
    SQL:     SQLSRSP01\LEEDS + VSDWH

## Creatio package VSIntegration
Системная настройка: Код: ScVsIntegrationApiUrl
    https://isexcd01.kolbasa-vs.ru:4432
