# Web URLs
kolbasa-vs.local

## IIS-сервера
IIS-DEV (10.0.204.17)
IIS-PROD (10.0.24.19)
CREATIO (10.0.24.24)
PALYCH (10.0.204.24)

## SQL-сервера
CREATIO\INS1 (10.0.24.24)		CREATIO, SCALES, VSDWH
PALYCH\LUTON (10.0.204.24)		ScalesDB
SQLSRSP01\LEEDS (10.0.204.15)	VSDWH
DEV1C\DVLP (10.0.204.20)		1C-PROD

## Веб-сервис "Обмен с весовыми постами" / WebApiScales
x IIS: CREATIO (10.0.24.24) -> поменять на -> IIS-DEV (10.0.204.17)
Предварительное тестирование	https://scales-dev-preview:443/
Рабочее тестирование			https://scales-dev:443/
✓ IIS: IIS-PROD (10.0.24.19)
Предварительный релиз			https://scales-prod-preview:443/
Рабочий релиз					https://scales-prod:443/

## Веб-сервис "Обмен с DWH" / WebApiTerra1000
x IIS: CREATIO (10.0.24.24) -> поменять на -> IIS-DEV (10.0.204.17)
Предварительное тестирование	https://t1000-dev-preview:443/
Рабочее тестирование			https://t1000-dev:443/
✓ IIS: IIS-PROD (10.0.24.19)
Предварительный релиз			https://t1000-preview:443/
Рабочий релиз					https://t1000:443/

## Веб-приложение "Управление устройствами" / BlazorDeviceControl
✓ IIS: IIS-DEV (10.0.204.17)
Предварительное тестирование	https://device-control-dev-preview/
Рабочее тестирование			https://device-control-dev/
x IIS: PALYCH (10.0.204.24) -> поменять на -> IIS-PROD (10.0.24.19)
Предварительный релиз			https://device-control-prod-preview/
Рабочий релиз					https://device-control/
