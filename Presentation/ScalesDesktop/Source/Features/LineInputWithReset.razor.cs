using Microsoft.AspNetCore.Components;
using ScalesDesktop.Source.Shared.Services;

namespace ScalesDesktop.Source.Features;

public sealed partial class LineInputWithReset : ComponentBase
{
    [Inject] private LineContext LineContext { get; set; } = null!;
}