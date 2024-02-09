using Microsoft.AspNetCore.Components;

namespace DeviceControl.Features.Sections.Shared.Form;

public sealed partial class SectionFormInputMultiSelect<TItem> : SectionFormInputBase where TItem : new()
{
    [Parameter, EditorRequired] public IEnumerable<TItem> Items { get; set; } = new List<TItem>();
    [Parameter, EditorRequired] public IEnumerable<TItem> SelectedItems { get; set; } = [];
    [Parameter, EditorRequired] public Func<TItem, string> ItemDisplayName { get; set; } = item => item!.ToString()!;
    [Parameter] public bool IsDisabled { get; set; }
    [Parameter] public EventCallback<IEnumerable<TItem>> SelectedItemsChanged { get; set; }

    private async Task OnSelectedItemsChanged() => await SelectedItemsChanged.InvokeAsync(SelectedItems);
}