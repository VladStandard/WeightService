namespace DeviceControl.Components;

public partial class TItemContent<TItem>
{
    [Parameter] public RenderFragment ChildContent { get; set; }
}
