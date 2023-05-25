namespace DeviceControl.Pages.CustomComponents;

public partial class TItemContent<TItem>
{
    [Parameter] public RenderFragment ChildContent { get; set; }
}
