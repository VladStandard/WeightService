// ReSharper disable ClassNeverInstantiated.Global

using Microsoft.AspNetCore.Components;
using ScalesHybrid.Services;

namespace ScalesHybrid.Features.Labels;

public sealed partial class LabelsPage : ComponentBase, IDisposable
{
    [Inject] public LabelContext LabelContext { get; set; } = null!;

    protected override void OnInitialized() => LabelContext.InitializeData();

    public void Dispose() => LabelContext.InitializeData();
}