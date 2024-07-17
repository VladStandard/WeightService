using System.ComponentModel;

namespace Ws.Shared.Enums;

public enum ArmType
{
    [Description("ArmTypeEnumPc")]
    Pc,
    [Description("ArmTypeEnumTablet")]
    Tablet,
    [Description("ArmTypeEnumUniversal")]
    Universal
}