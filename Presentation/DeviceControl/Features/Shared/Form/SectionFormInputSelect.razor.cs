using Microsoft.AspNetCore.Components;

namespace DeviceControl.Features.Shared.Form;

public sealed partial class SectionFormInputSelect<TItem>: ComponentBase where TItem: new()
{
    [Parameter] public string Title { get; set; } = string.Empty;
    [Parameter, EditorRequired] public IEnumerable<TItem> Items { get; set; } = new List<TItem>();
    [Parameter, EditorRequired] public TItem SelectedItem { get; set; } = new();
    [Parameter, EditorRequired] public Func<TItem, string> ItemDisplayName { get; set; } = item => item!.ToString()!;
    [Parameter, EditorRequired] public Func<TItem, string> ItemValue { get; set; } = item => item!.ToString()!;
    [Parameter, EditorRequired] public Func<string, TItem> IdentifyItemOnValueChange { get; set; } = null!;
    
    private string SelectedItemValue { get; set; } = string.Empty;

    protected override void OnInitialized()
    {
        SelectedItemValue = ItemValue(SelectedItem);
    }
}
