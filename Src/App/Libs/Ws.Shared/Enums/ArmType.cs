using System.ComponentModel;

namespace Ws.Shared.Enums;

public enum ArmType
{
    [Description("ArmPc")]
    Pc,
    [Description("ArmTablet")]
    Tablet,
    [Description("ArmUniversal")]
    Universal
}