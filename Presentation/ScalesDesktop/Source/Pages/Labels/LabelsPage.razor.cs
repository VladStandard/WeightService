// ReSharper disable ClassNeverInstantiated.Global

using Microsoft.AspNetCore.Components;
using ScalesDesktop.Source.Shared.Services;

namespace ScalesDesktop.Source.Pages.Labels;

public sealed partial class LabelsPage : ComponentBase, IDisposable
{
    # region Injects

    [Inject] private LabelContext LabelContext { get; set; } = default!;
    [Inject] private LineContext LineContext { get; set; } = default!;

    # endregion

    protected override void OnInitialized()
    {
        LabelContext.InitializeData();
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
            LineContext.ConnectScale();
    }

    public void Dispose()
    {
        LineContext.Dispose();
    }
}