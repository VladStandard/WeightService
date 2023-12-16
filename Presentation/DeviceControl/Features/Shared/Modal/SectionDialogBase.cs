using Force.DeepCloner;
using Microsoft.AspNetCore.Components;
using Ws.Shared.Enums;

namespace DeviceControl.Features.Shared.Modal;

public class SectionDialogBase<TItem>: ComponentBase where TItem: new()
{
    [Parameter] public TItem DialogSectionEntity { get; set; } = new();
    [Parameter] public EventCallback OnDataChangedAction { get; set; }
    
    public TItem SectionEntity { get; set; } = new();

    protected override void OnInitialized()
    {
        SectionEntity = DialogSectionEntity.DeepClone();
    }

    protected List<EnumTypeModel<string>> TabsList { get; set; } = new() { new("Main", "main") };
}