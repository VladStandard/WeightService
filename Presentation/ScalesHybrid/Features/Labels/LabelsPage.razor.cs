using Microsoft.AspNetCore.Components;
using ScalesHybrid.Services;

namespace ScalesHybrid.Features.Labels;

public sealed partial class LabelsPage : ComponentBase
{
    [Inject] private LineContext LineContext { get; set; } = null!;
}