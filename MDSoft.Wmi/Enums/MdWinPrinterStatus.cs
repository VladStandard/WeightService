// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/win32-printer

namespace MDSoft.Wmi.Enums;

public enum MdWinPrinterStatus
{
    Idle = 0,
    Paused = 1,
    Error = 2,
    PendingDeletion = 3,
    PaperJam = 4,
    PaperOut = 5,
    ManualFeed = 6,
    PaperProblem = 7,
    Offline = 8,
    IoActive = 9,
    Busy = 10,
    Printing = 11,
    OutputBinFull = 12,
    NotAvailable = 13,
    Waiting = 14,
    Processing = 15,
    Initialization = 16,
    WarmingUp = 17,
    TonerLow = 18,
    NoToner = 19,
    PagePunt = 20,
    UserInterventionRequired = 21,
    OutOfMemory = 22,
    DoorOpen = 23,
    ServerUnknown = 24,
    PowerSave = 25,
}