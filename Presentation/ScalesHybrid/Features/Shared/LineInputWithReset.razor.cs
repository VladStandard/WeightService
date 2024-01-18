using Microsoft.AspNetCore.Components;
using ScalesHybrid.Services;

namespace ScalesHybrid.Features.Shared;

public sealed partial class LineInputWithReset: ComponentBase
{
    [Inject] private LineContext LineContext { get; set; } = null!;
}