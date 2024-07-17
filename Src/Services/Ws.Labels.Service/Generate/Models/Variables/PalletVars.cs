using System.Diagnostics.CodeAnalysis;

namespace Ws.Labels.Service.Generate.Models.Variables;

/// <summary>
/// DON'T TOUCH (THIS IS VARIABLES FOR TEMPLATES) BE CAREFUL
/// </summary>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public record PalletVars
{
    public required string Number;
    public required ushort Order;
}