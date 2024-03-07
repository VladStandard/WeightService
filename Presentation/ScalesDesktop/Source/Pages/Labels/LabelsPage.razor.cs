// ReSharper disable ClassNeverInstantiated.Global

using Microsoft.AspNetCore.Components;
using ScalesDesktop.Source.Shared.Services;

namespace ScalesDesktop.Source.Pages.Labels;

public sealed partial class LabelsPage : ComponentBase, IDisposable
{
    [Inject] public LabelContext LabelContext { get; set; } = null!;
    [Inject] public LineContext LineContext { get; set; } = null!;
    
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