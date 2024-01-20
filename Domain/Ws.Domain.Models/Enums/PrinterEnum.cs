using System.ComponentModel;

namespace Ws.Domain.Models.Enums;

public enum PrinterTypeEnum
{
    [Description("Tsc")]
    Tsc,
    [Description("Zebra")]
    Zebra,
}