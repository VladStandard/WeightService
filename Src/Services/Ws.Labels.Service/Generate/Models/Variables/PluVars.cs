using System.Diagnostics.CodeAnalysis;

namespace Ws.Labels.Service.Generate.Models.Variables;

/// <summary>
/// DON'T TOUCH (THIS IS VARIABLES FOR TEMPLATES) BE CAREFUL
/// </summary>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public record PluVars
{
    public required string Name;
    public required ushort Number;
    public required string Description;
}