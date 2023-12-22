namespace DeviceControl.Components.Item;

public partial class ItemTopBar<TItem> : ComponentBase where TItem : SqlEntityBase, new()
{
    [Parameter] public EventCallback OnItemUpdate { get; set; }
    [Parameter] public string Title { get; set; }
}
