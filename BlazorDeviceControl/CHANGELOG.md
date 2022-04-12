# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [0.6.440] - 2022-04-12
### Fixed
- Shared.ItemPrinter
- Shared.ItemPrinterResource
- Shared.ItemPrinterType
- Shared.SectionPrinterResources
- Shared.SectionPrinters
- Shared.SectionPrinterTypes
### Описание обновления:
- Раздел и страница "Принтеры"
- Раздел и страница "Ресурсы принтера"
- Страница "Ресурсы принтера": Удалить ресурсы, загрузить шрифты, загрузить картинки

## [0.6.420] - 2022-04-07
### Fixed
- Shared.MainLayout
- Shared.NavMenu
### Описание обновления:
- Новое меню слева

## [0.6.410] - 2022-04-06
### Fixed
- Item PLU page
- Shared.MainLayout
- Shared.NavMenu
### Описание обновления:
- Страница "ПЛУ": расположение полей
- Выравнивание элементов на галвной странице

## [0.6.400] - 2022-04-05
### Fixed
- Section and item Scale page
### Описание обновления:
- Раздел и страница "Устройства"

## [0.6.390] - 2022-04-01
### Fixed
- Item PLU page
- Item Template page
### Описание обновления:
- Cтраница "ПЛУ": исправлена связь поля "Устройство"
- Раздел и страница "Шаблоны": создание новой записи

## [0.6.370] - 2022-03-30
### Fixed
- Sections pages
- Items pages
### Описание обновления:
- Восстановлено ограничение доступа к приложению для неавторизованных пользователей.
- Права доступа можно редактировать из приложения.
- Добавлены прямые ссылки перехода из разделов в запись.
- Раздел и страница "Устройства": исправлены ошибки изменения и сохранения значений записи.
- Раздел и страница "Принтеры": добавлено поле статуса устройства (доступность по сети).

## [0.6.350] - 2022-03-24
### Fixed
- WS-T-107. DeviceControl. Access rights hotfix

## [0.6.330] - 2022-03-24
### Changed
- Shared.Index
- Shared.MainLayout

## [0.6.320] - 2022-03-22
### Changed
- Shared.Section.Access
- Shared.Section.Barcodes
- Shared.Section.BarcodeTypes
- Shared.Section.Contragents
- Shared.Item.Access
- Shared.Item.Barcodes
- Shared.Item.BarcodeTypes
- Shared.Item.Contragents

## [0.6.220] - 2022-02-22
### Changed
- Shared.Section.Nomenclatures
- Shared.Section.Plus
- Shared.Item.Nomenclature
- Shared.Item.Plu

## [0.6.210] - 2022-02-21
### Changed
- Shared.Section: _locker object
- Shared.Item: _locker object

## [0.6.200] - 2022-02-18
### Changed
- Shared.Item.Printer
- Shared.Item.Scale
- Shared.Section.Info

## [0.6.110] - 2022-02-02
### Changed
- Moved system sections and items componennts

## [0.6.080] - 2022-01-28
### Changed
- ActionsButtons
- ActionsFilter
- ActionsLoad
- ActionsReload
- ActionsSave
- ItemDates

## [0.6.050] - 2022-01-21
### Added
- WS-T-57. Info DB size

## [0.6.040] - 2022-01-19
### Fixed
- BlazorCore refactoring
### Added
- WS-T-67. Host section edit

## [0.5.930] - 2021-12-14
### Fixed
- MemoryEntity

## [0.5.910] - 2021-12-03
### Changed
- Fixed issues

## [0.5.880] - 2021-11-25
### Changed
- WS-T-32. BlazorDeviceControl. Refactoring

## [0.5.470] - 2021-09-01
### Changed
- Shared.Section.Printers

## [0.5.440] - 2021-08-23
### Changed
- BlazorCore.Utils.LocalizationStrings
- Shared.Item.Scale
- Shared.Sys.Info
- Shared.Sys.Logs
- Shared.Section.Scales
### Added
- Shared.Item.EntityActions
- Shared.Section.ActionsButtons

## [0.5.380] - 2021-08-13
### Changed
- Refactoring
- Docs.razor
- Memory optimization
### Added
- Language switch between English and Russian

## [0.2.320] - 2021-07-28
### Changed
- Razor pages

## [0.2.300] - 2021-07-27
### Changed
- Debug DB location

## [0.2.280] - 2021-07-20
### Added
- Authentication

## [0.2.270] - 2021-07-19
### Changed
- Scales section
- Logs section

## [0.2.260] - 2021-07-15
### Added
- Logs section

## [0.2.230] - 2021-07-13
### Changed
- WeithingFacts section
- Scales section
- Hosts section

## [0.2.220] - 2021-07-06
### Added
- WeithingFacts section

## [0.2.200] - 2021-05-25
### Added
- MemorySize control

## [0.2.190] - 2021-05-12
### Changed
- Plu record page

## [0.2.180] - 2021-05-11
### Changed
- Get free hosts
- Get busy hosts
### Added
- Exceptions logging
- drop index if exists [IDX_Scales_HostId] on [db_scales].[Scales]
