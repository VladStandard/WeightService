# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [0.6.520] - 2022-04-21
### Changed
- MainForm
- DataCore.Managers
### Описание обновления:
- Оптимизация отклика главной формы.
- Оптимизация отклика взвешивания.
- Оптимизация печати.

## [0.6.460] - 2022-04-14
### Changed
- MainForm

## [0.6.450] - 2022-04-13
### Changed
- MainForm
### Описание обновления:
- Динамическое создание кнопок на главной форме.

## [0.6.410] - 2022-04-06
### Changed
- MainForm
### Описание обновления:
- Адаптивные шрифты под разрешение.
- Пороговые значения веса.

## [0.6.400] - 2022-04-05
### Описание обновления:
- Запрос веса только для весовых PLU.
- Режим отображения кнопок.
- Открытие вэб-приложения "Управление устройствами".
- Код транспортной упаковки (SSCC).
- Корректировка печати весовых этикеток "Чудо-Печка".

## [0.6.380] - 2022-03-31
### Fixed
- HotFix для Чудо-Печка.

## [0.6.210] - 2022-02-21
### Fixed
- GUI main form

## [0.6.200] - 2022-02-18
### Fixed
- TSC print

## [0.6.130] - 2022-02-03
### Added
- Loading references Properties.Settings.Default.ConnectionString depending on debugging mode

## [0.6.030] - 2022-01-17
### Fixed
- PVS-Studio analyze
- Exit image

## [0.5.920] - 2021-12-07
### Fixed
- WS-T-45. Ошибка отключения принтера Zebra

## [0.5.900] - 2021-11-29
### Fixed
- Ошибка печати
- WS-T-34. Exception WPF window

## [0.5.860] - 2021-11-22
### Fixed
- WS-T-26. SQL-script переноса PLU между устройствами
- WS-T-27. Localization GUI
- WS-T-29. WeightCore. XAML PageMessageBox

## [0.5.820] - 2021-11-16
### Fixed
- WS-T-21. Обработка разрыва связи в весами Масса-К
- WS-T-22. Выделить задачу наблюдения за принтером
- WS-T-23. Выделить задачу наблюдения за памятью
- WS-T-24. Локализация CustomMessageBox
- WS-T-25. Устранить зависание документа печати принтера TSC

## [0.5.810] - 2021-11-15
### Fixed
- WS-T-18. ScalesUI. Выделить задачу наблюдения за состоянием COM-порта
- WS-T-19. ScalesUI. Запретить указывать более 1 этикетки для весовой продукции

## [0.5.800] - 2021-11-12
### Fixed
- Состояние весов
- Состояние принтера
- Quartz schedulers

## [0.5.750] - 2021-11-02
### Fixed
- MassaK scales
### Added
- Run ScalesTerminal
- Protokol 100

## [0.5.690] - 2021-10-26
### Fixed
- TSC printer

## [0.5.680] - 2021-10-22
### Fixed
- TSC printer

## [0.5.590] - 2021-09-13
### Fixed
- Redmine 1605. После наступления новых суток, требуется изменить дату производства

## [0.5.580] - 2021-09-13
### Fixed
- Redmine 1540. Печать 1 этикетки после серии
- Redmine 1602. Ошибка логирования этикеток
- Redmine 1603. Логирование взвешиваний происходит некорректно

## [0.5.570] - 2021-09-10
### Fixed
- Redmine 1601. Менеджер задач. Тестирование и отладка

## [0.5.560] - 2021-09-09
### Fixed
- Redmine 1585. Некорректная информация об очереди печати
- Redmine 1604. Добавить возможность откатывать дату

## [0.5.540] - 2021-09-06
### Fixed
- Redmine 1591. Рефакторинг, объединение проектов
- Redmine 1588. Ошибка запуска окна настроек
- Redmine 1597. Некорректные данные на этикетке

## [0.5.480] - 2021-09-02
### Fixed
- Redmine 1588. Ошибка запуска окна настроек

## [0.5.470] - 2021-09-01
### Fixed
- Creatio SR00018265. ЧудоПечка печатать этикеток без данных

## [0.5.371] - 2021-08-12
### Changed
- Downgrade .NET Framework to v.4.7.2
- Fixed SqlConnection queries
### Added
- WPF PageSqlsettings
- Task settings

## [0.5.310] - 2021-07-27
### Changed
- Debug DB location
- Log errors into DB

## [0.5.260] - 2021-07-15
### Changed
- LogEntity

## [0.5.240] - 2021-07-14
### Added
- LogEntity

## [0.5.210] - 2021-07-02
### Changed
- Skip weight control for unit

## [0.5.190] - 2021-06-29
### Changed
- MainForm
- UserSession
- CustomMessageBox
- MainForm PLU label
### Added
- Штучные этикетки для "Чудо Печка"

## [0.5.160] - 2021-06-24
### Added
- PluList XAML page

## [0.5.150] - 2021-06-04
### Changed
- Tasks managers
- New PLU parse EAN13

## [0.5.100] - 2021-05-27
### Changed
- Print checked

## [0.5.90] - 2021-05-26
### Changed
- Print TSC methods

## [0.5.50] - 2021-05-10
### Changed
- ZplCommonLib

## [0.5.40] - 2021-05-06
### Changed
- ZplCommonLib

## [0.5.02] - [0.5.34]
### Changed
- Skipped descriptions

## [0.5.01] - 2021-02-15
- First version in 2021 yaer

## [0.4.21] - 2020-10-26
### Added
- Comments to modules
### Changed
- Program
- MouseHookHelper
### Task
- 1345. Появляется окно выбора PLU после печати с кнопки

## [0.4.6] - 2020-10-19
### Added
- Настройка для релиза "-admin" позволяет имитировать отладочный режим
### Changed
- Program

## [0.4.5] - 2020-10-15
### Added
- Ссылка на ZabbixAgentLib 0.1.30
- Открытие http-страницы при двойном клике на контроле Zabbix
### Changed
- MainForm

## [0.3.96] - 2020-10-09
### Added
- WeightServices.Common.LogHelper
### Changed
- MassaKLib 0.03.96

## [0.3.90] - 2020-10-08
### Added
- Newtonsoft.Json.dll
- SdkApi.Core.dll
- SdkApi.Desktop.dll

## [0.3.80] - 2020-10-07
### Added
- UICommonLib\NotifyStateEntity to MainForm
### Changed
- Updated log4net version to 2.0.11
- Program
- MainForm
