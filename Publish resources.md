# Publish resources

## Шаблон рассылки обновления
Коллеги, выпустил обновление веб-приложения DeviceControl v.0.x.xxx.
Прошу протестировать. Замечания слать на почту.
Для тестирования использовать следующие ссылки:
https://device-control-dev.kolbasa-vs.local/           ## стабильная версия веб-приложения DeviceControl для тестирования
https://device-control-dev-preview.kolbasa-vs.local/   ## предварительная версия веб-приложения DeviceControl для тестирования
Для рабочей эксплуатации использовать следующие ссылки:
https://device-control.kolbasa-vs.local/               ## стабильная версия веб-приложения DeviceControl для работы
https://device-control-prod-preview.kolbasa-vs.local/  ## предварительная версия веб-приложения DeviceControl для работы

## Шаблон рассылки обновления
Коллеги, выпустил обновление веб-приложения DeviceControl v.0.x.xxx.
Прошу протестировать. Замечания слать на почту.
Для тестирования использовать следующие ссылки:
https://device-control-dev-preview.kolbasa-vs.local/   ## тестовая среда
https://device-control-prod-preview.kolbasa-vs.local/  ## рабочая среда

## DeviceControl
  Debug:     
             IIS-DEV (10.0.204.17): https://device-control-dev.kolbasa-vs.local/
             IIS-DEV (10.0.204.17): https://device-control-dev-preview.kolbasa-vs.local/
    SQL:     CREATIO\INS1 + SCALES
    DB:      SCALES
    RDP:     mstsc /v:iis-dev
    CONFIG:  appsettings.Debug.json -> appsettings.json
    IIS:     creatio:5009 + device-test
  Release:   PALYCH (10.0.204.24):  https://device-control.kolbasa-vs.local/
             PALYCH (10.0.204.24):  https://device-control-prod.kolbasa-vs.local/
             PALYCH (10.0.204.24):  https://device-control-prod-preview.kolbasa-vs.local/
    SQL:     PALYCH\LUTON
    DB:      ScalesDB
    RDP:     mstsc /v:palych
    CONFIG:  appsettings.Release.json -> appsettings.json
    IIS:     palych + device-control

## BlazorResourcesVs
  Debug:     https://resources-test.kolbasa-vs.local/
    SQL:     
    DB:      
    RDP:     mstsc /v:creatio
    CONFIG:  appsettings.Debug.json -> appsettings.json
    IIS:     creatio + resources-test
  Release:   https://resources-vs.kolbasa-vs.local/
    SQL:     PALYCH\LUTON
    DB:      ScalesDB
    RDP:     mstsc /v:palych
    CONFIG:  appsettings.Release.json -> appsettings.json
    IIS:     palych + resources-vs

## ScalesUI
  Debug:     switch to Debug
    Fodler:  \\palych\Install\MSI\ScalesUIv3-Debug\
    SQL:     CREATIO\INS1 + SCALES
  Release:   switch to Relase
    Folder:  \\palych\Install\MSI\ScalesUI-Chudo\
    Folder:  \\palych\Install\MSI\ScalesUI-Release\
    SQL:     PALYCH\LUTON + ScalesDB

## MdmControl
  Debug:     ftp://mdm-test:5007 -> https://mdm-test.kolbasa-vs.local/
    SQL:     CREATIO\INS1 + VSDWH
    RDP:     mstsc /v:creatio
    CONFIG:  appsettings.Debug.json -> appsettings.json
  Release:   ftp://mdm-dwh:5005  -> https://mdm-dwh.kolbasa-vs.local/
    SQL:     SQLSRSP01\LEEDS + VSDWH
    RDP:     mstsc /v:isexcd01
    CONFIG:  appsettings.Release.json -> appsettings.json

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
