using Force.DeepCloner;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Ws.Domain.Models.Entities.Ref;

namespace DeviceControl.Source.Widgets.Section.Dialogs;

[CascadingTypeParameter(nameof(TDialogItem))]

public abstract class SectionDialogWithProductionSite<TDialogItem> : ComponentBase,
    IDialogContentComponent<SectionDialogContentWithProductionSite<TDialogItem>>
{
    [Parameter] public SectionDialogContentWithProductionSite<TDialogItem> Content { get; set; } = default!;
    protected TDialogItem DialogItem { get; private set; } = default!;
    protected List<KeyValuePair<Type, string>> TabsList { get; private set; } = [];

    protected override void OnInitialized()
    {
        DialogItem = Content.Item.DeepClone();
        TabsList = InitializeTabList();
    }

    protected abstract List<KeyValuePair<Type, string>> InitializeTabList();
}

public record SectionDialogContentWithProductionSite<TDialogItem> : SectionDialogContent<TDialogItem>
{
    public required ProductionSiteEntity ProductionSite { get; init; } = default!;
}