// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ScalesUI.Utils
{
    /// <summary>
    /// Настройки по-умолчанию.
    /// </summary>
    public enum EnumDefaultSetting
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

    /// <summary>
    /// UI без-взаимодействия с пользователем.
    /// </summary>
    public enum EnumSilentUI
    {
        True,
        False,
    }

    public enum Direction
    {
        Forward,
        Back
    }
}
