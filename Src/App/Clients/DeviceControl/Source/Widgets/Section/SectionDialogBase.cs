using Force.DeepCloner;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Ws.Shared.Enums;

namespace DeviceControl.Source.Widgets.Section;

[CascadingTypeParameter(nameof(TDialogItem))]

public class SectionDialogBase<TDialogItem> : ComponentBase, IDialogContentComponent<SectionDialogContent<TDialogItem>>
{
    [Parameter] public SectionDialogContent<TDialogItem> Content { get; set; } = default!;
    protected TDialogItem DialogItem { get; private set; } = default!;
    protected List<KeyValuePair<string, string>> TabsList { get; private set; } = [];

    protected override void OnInitialized()
    {
        DialogItem = Content.Item.DeepClone();
        TabsList = InitializeTabList();
    }

    protected virtual List<KeyValuePair<string, string>> InitializeTabList() =>
        [new("Main", "main")];
}

public record SectionDialogContent<TDialogItem>
{
    public TDialogItem Item { get; init; } = default!;
    public SectionDialogResultEnum DataAction { get; init; } = SectionDialogResultEnum.Cancel;
}