using Force.DeepCloner;
using Microsoft.AspNetCore.Components;
using Ws.Shared.Enums;

namespace DeviceControl.Features.Sections.Shared.Modal;

public class SectionDialogBase<TItem>: ComponentBase where TItem: new()
{
    [Parameter] public TItem DialogSectionEntity { get; set; } = new();
    [Parameter] public EventCallback OnDataChangedAction { get; set; }
    
    public TItem SectionEntity { get; set; } = new();
    protected List<EnumTypeModel<string>> TabsList { get; set; } = [];

    protected override void OnInitialized()
    {
        SectionEntity = DialogSectionEntity.DeepClone();
        TabsList = InitializeTabList();
    }

    protected virtual List<EnumTypeModel<string>> InitializeTabList() =>
        [new("Main", "main")];
}