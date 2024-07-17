using System.Diagnostics.CodeAnalysis;

namespace Ws.Labels.Service.Generate.Models.Variables;

/// <summary>
/// DON'T TOUCH (THIS IS VARIABLES FOR TEMPLATES) BE CAREFUL
/// </summary>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public record ArmVars
{
    public required int Number;
    public required string Name;
    public required string Address;
}