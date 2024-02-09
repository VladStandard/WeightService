using Microsoft.AspNetCore.Components;

namespace DeviceControl.Features.Sections.Shared.Form;

public sealed partial class SectionFormInputSelect<TItem> : SectionFormInputBase where TItem : new()
{
    [Parameter, EditorRequired] public IEnumerable<TItem> Items { get; set; } = new List<TItem>();
    [Parameter, EditorRequired] public Func<TItem, string> ItemDisplayName { get; set; } = item => item!.ToString()!;
    [Parameter, EditorRequired] public TItem SelectedItem { get; set; } = new();
    [Parameter] public EventCallback<TItem> SelectedItemChanged { get; set; }
    [Parameter] public bool IsDisabled { get; set; }
    [Parameter] public bool IsFilterable { get; set; }


    private async Task OnSelectedItemChanged() => await SelectedItemChanged.InvokeAsync(SelectedItem);
}