using Microsoft.AspNetCore.Components;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;

namespace ScalesDesktop.Source.Widgets.LabelConfig;

public sealed partial class PluDescription : ComponentBase
{
    [Parameter] public PluEntity Plu { get; set; } = default!;
    [Parameter] public PluNestingEntity PluNesting { get; set; } = default!;
}