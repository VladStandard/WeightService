using Microsoft.AspNetCore.Components;

namespace ScalesHybrid.Components.Dialogs;

public sealed partial class DialogDataGridWrapper<TItem>: ComponentBase
{
    [Parameter] public IEnumerable<TItem> GridData { get; set; }
    [Parameter] public RenderFragment ChildContent { get; set; }
    [Parameter] public EventCallback<TItem> OnItemSelect { get; set; }
    [Parameter] public string Title { get; set; } = string.Empty;
}