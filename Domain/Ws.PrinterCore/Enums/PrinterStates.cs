namespace Ws.PrinterCore.Enums;

public enum PrinterStates
{
    Unknown = 0,
    Paused,
    ReadyToPrint,
    HeadOpen,
    PaperOut,
    HeadTooHot,
    HeadTooCold,
}