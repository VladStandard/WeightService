using System.ComponentModel;

namespace Ws.Domain.Models.Enums;

public enum PrinterTypes
{
    [Description("PrinterTsc")]
    Tsc,
    [Description("PrinterZebra")]
    Zebra
}