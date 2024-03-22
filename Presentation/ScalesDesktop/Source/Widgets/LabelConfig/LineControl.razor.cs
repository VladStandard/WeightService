using Microsoft.AspNetCore.Components;
using ScalesDesktop.Source.Shared.Services;

namespace ScalesDesktop.Source.Widgets.LabelConfig;

public sealed partial class LineControl : ComponentBase
{
    [Inject] private LineContext LineContext { get; set; } = null!;
    [Inject] private LabelContext LabelContext { get; set; } = null!;

    private void AppReset()
    {
        LineContext.ResetLine();
        LabelContext.InitializeData();
    }
}