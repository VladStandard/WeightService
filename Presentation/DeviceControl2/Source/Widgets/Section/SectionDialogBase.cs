using Force.DeepCloner;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Ws.Shared.Enums;

namespace DeviceControl2.Source.Widgets.Section;

public class SectionDialogBase<TItem> : ComponentBase, IDialogContentComponent<TItem> where TItem: new()
{
    [CascadingParameter] public FluentDialog Dialog { get; set; } = default!;
    [Parameter] public TItem Content { get; set; } = default!;
    protected TItem SectionEntity { get; private set; } = new();
    protected List<EnumTypeModel<string>> TabsList { get; private set; } = [];

    protected override void OnInitialized()
    {
        SectionEntity = Content.DeepClone();
        TabsList = InitializeTabList();
    }

    protected async Task CancelDialog() => await Dialog.CancelAsync();
    protected async Task CloseDialog() => await Dialog.CloseAsync();

    protected virtual List<EnumTypeModel<string>> InitializeTabList() =>
        [new("Main", "main")];
}