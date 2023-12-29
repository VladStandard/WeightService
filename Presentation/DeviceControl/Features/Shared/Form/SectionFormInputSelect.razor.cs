using Microsoft.AspNetCore.Components;

namespace DeviceControl.Features.Shared.Form;

public sealed partial class SectionFormInputSelect<TItem>: SectionFormInputBase where TItem: new()
{
    [Parameter, EditorRequired] public IEnumerable<TItem> Items { get; set; } = new List<TItem>();
    [Parameter, EditorRequired] public TItem SelectedItem { get; set; } = new();
    [Parameter, EditorRequired] public Func<TItem, string> ItemDisplayName { get; set; } = item => item!.ToString()!;
    [Parameter, EditorRequired] public Func<TItem, string> ItemValue { get; set; } = item => item!.ToString()!;
    [Parameter, EditorRequired] public Func<string, TItem> IdentifyItemOnValueChange { get; set; } = null!;
    [Parameter] public bool IsDisabled { get; set; }
    [Parameter] public EventCallback<TItem> SelectedItemChanged { get; set; }
    
    private string SelectedItemValue { get; set; } = string.Empty;
    
    protected override void OnParametersSet()
    {
        string newValue = ItemValue(SelectedItem);
        if (!SelectedItemValue.Equals(newValue))
            SelectedItemValue = newValue;
    }
    
    private async void HandleValueChange(string value)
    {
        SelectedItemValue = value;
        SelectedItem = IdentifyItemOnValueChange(value);
        await SelectedItemChanged.InvokeAsync(SelectedItem);
    }
}
