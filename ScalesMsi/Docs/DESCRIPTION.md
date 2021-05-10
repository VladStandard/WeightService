# WiX


## Необходимое ПО

### WiX Toolset
https://wixtoolset.org/releases/
https://github.com/wixtoolset/wix3/releases/tag/wix3112rtm

### WixUI_ru-RU.wxl
https://wixtoolset.org/documentation/manual/v3/wixui/wixui_localization.html

### Wix Toolset Visual Studio 2019 Extension
https://marketplace.visualstudio.com/items?itemName=WixToolset.WixToolsetVisualStudio2019Extension
### Wix Toolset Visual Studio 2017 Extension
https://marketplace.visualstudio.com/items?itemName=WixToolset.WixToolsetVisualStudio2017Extension

### IsWiX
https://github.com/iswix-llc/iswix/releases

### WixSharp
https://www.nuget.org/packages/WixSharp/

## Статьи

### IsWiX Desktop Application Tutorial
https://github.com/iswix-llc/iswix-tutorials/tree/master/desktop-application

### Создание инсталлятора с помощью WiX
https://habr.com/ru/post/68616/

### Использование C# и Wix# для создания msi-пакетов
https://habr.com/ru/post/253819/

### Автоматическая установка и настройка PostgreSQL при помощи Wix#
https://m.habr.com/ru/post/276175/

### Поиск драйвера в PowerShell
 gwmi -Class Win32_Product | select identifyingnumber, name, vendor, version, caption | where { $_ -match "Virtual Comport Driver" }

### Добавление dll в MSI
https://csharp.hotexamples.com/ru/examples/WixSharp/ManagedAction/-/php-managedaction-class-examples.html
