using Force.DeepCloner;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Ws.Shared.Enums;

namespace DeviceControl2.Source.Widgets.Section;

public class SectionDialogBase<TItem> : ComponentBase, IDialogContentComponent<TItem> where TItem: new()
{
    [Parameter] public TItem Content { get; set; } = default!;
    public TItem SectionEntity { get; set; } = new();
    protected List<EnumTypeModel<string>> TabsList { get; set; } = [];

    protected override void OnInitialized()
    {
        SectionEntity = Content.DeepClone();
        TabsList = InitializeTabList();
    }

    protected virtual List<EnumTypeModel<string>> InitializeTabList() =>
        [new("Main", "main")];
}