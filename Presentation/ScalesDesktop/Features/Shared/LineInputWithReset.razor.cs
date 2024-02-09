using Microsoft.AspNetCore.Components;
using ScalesDesktop.Services;

namespace ScalesDesktop.Features.Shared;

public sealed partial class LineInputWithReset : ComponentBase
{
    [Inject] private LineContext LineContext { get; set; } = null!;
}