using System.ComponentModel;

namespace Ws.Domain.Models.Enums;

// DON'T touch description (used for localization)
public enum ArmTypes
{
    [Description("ArmPc")]
    Pc,
    [Description("ArmTablet")]
    Tablet,
    [Description("ArmUniversal")]
    Universal
}