// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Models;

public enum DefaultSettingEnum
{
    All,
    ComPortName,
    SendTimeout,
    ReceiveTimeout,
    ZebraTcpAddress,
    ZebraTcpPort,
    Description,
    Guid,
    ConnectionString
}

public enum SilentUiEnum
{
    True,
    False
}

public enum DirectionEnum
{
    Left,
    Right
}

public enum PageEnum
{
    Default,
    MessageBox,
    PinCode,
    Device,
    SqlSettings,
    PluBundleFk
}

public enum OrderStatusEnum
{
    New = 0,
    InProgress = 1,
    Paused = 2,
    Performed = 3,
    Canceled = 4
}

public enum WpfActivePageEnum
{
    About,
    ChangeLog,
    PinCode,
    Home,
    Settings
}

/// <summary>
/// Сообщение мыши.
/// </summary>
public enum MouseMessagesEnum
{
    WM_LBUTTONDOWN = 0x0201,
    WM_LBUTTONUP = 0x0202,
    WM_MOUSEMOVE = 0x0200,
    WM_MOUSEWHEEL = 0x020A,
    WM_RBUTTONDOWN = 0x0204,
    WM_RBUTTONUP = 0x0205,
    WM_MBUTTONDOWN = 0x0207
}

/// <summary>
/// Параметры отображения окна.
/// </summary>
public enum CmdShowEnum
{
    SW_HIDE, // Скрывает окно и активизирует другое окно.
    SW_MAXIMIZE, // Развертывает определяемое окно.
    SW_MINIMIZE, // Свертывает определяемое окно и активизирует следующее окно верхнего уровня в Z-последовательности.
    SW_RESTORE, // Активизирует и отображает окно. Если окно свернуто или развернуто, Windows восстанавливает в его первоначальных размерах и позиции.

    // Прикладная программа должна установить этот флажок при восстановлении свернутого окна.
    SW_SHOW, //  Активизирует окно и отображает его текущие размеры и позицию.
    SW_SHOWDEFAULT, // Устанавливает состояние показа, основанное на флажке SW_, определенном в структуре STARTUPINFO, 

    // переданной в функцию CreateProcess программой, которая запустила прикладную программу.
    SW_SHOWMAXIMIZED, //  Активизирует окно и отображает его как развернутое окно.
    SW_SHOWMINIMIZED, // Активизирует окно и отображает его как свернутое окно.
    SW_SHOWMINNOACTIVE, // Отображает окно как свернутое окно. Активное окно остается активным.
    SW_SHOWNA, // Отображает окно в его текущем состоянии. Активное окно остается активным.
    SW_SHOWNOACTIVATE, // Отображает окно в его самом современном размере и позиции. Активное окно остается активным.
    SW_SHOWNORMAL //  Активизирует и отображает окно. Если окно свернуто или развернуто, Windows восстанавливает его в первоначальном размере и позиции.
                   // Прикладная программа должна установить этот флажок при отображении окна впервые.
}

/// <summary>
/// Действие принтера Зебры.
/// </summary>
public enum ZebraActionEnum
{
    /// <summary>
    /// Сбросить принтер.
    /// </summary>
    Reset,
    /// <summary>
    /// Калибровать принтер.
    /// </summary>
    Calibrate,
    /// <summary>
    /// Список шрифтов.
    /// </summary>
    FontsList,
    /// <summary>
    /// Загрузить шрифты.
    /// </summary>
    LoadFonts,
    /// <summary>
    /// Очистить шрифты.
    /// </summary>
    ClearFonts,
    /// <summary>
    /// ~WC.
    /// </summary>
    WC,
    /// <summary>
    /// Загрузить картинки.
    /// </summary>
    LoadImages
}

/// <summary>
/// Уровень подробности строки подключения.
/// </summary>
public enum ConStringLevelEnum
{
    Basic,
    Low,
    Middle,
    Full
}
