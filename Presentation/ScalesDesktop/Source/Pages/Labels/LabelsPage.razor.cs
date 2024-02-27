// ReSharper disable ClassNeverInstantiated.Global

using Microsoft.AspNetCore.Components;
using ScalesDesktop.Source.Shared.Services;

namespace ScalesDesktop.Source.Pages.Labels;

public sealed partial class LabelsPage : ComponentBase, IDisposable
{
    [Inject] public LabelContext LabelContext { get; set; } = null!;

    protected override void OnInitialized() => LabelContext.InitializeData();

    public void Dispose() => LabelContext.InitializeData();
}