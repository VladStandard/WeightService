namespace Ws.Printers.Enums;

public enum PrinterStatusEnum
{
    IsDisabled, // отключен
    IsForceDisconnected, // принтер не в сети
    Unknown,
    Paused,
    Ready,
    HeadOpen, // открыта крышка
    PaperOut, // кончилась лента
    PaperJam, // замяло этикетку
    Busy, // занят
    RibbonOut, // замяло что то
}