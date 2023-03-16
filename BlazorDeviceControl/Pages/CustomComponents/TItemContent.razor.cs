namespace BlazorDeviceControl.Pages.CustomComponents;

public partial class TItemContent<TItem> : LayoutComponentBase
{
    [Parameter] public RenderFragment ChildContent { get; set; }
}