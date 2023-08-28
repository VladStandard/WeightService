namespace DeviceControl.Components.Item;

public partial class ItemTopBar<TItem> : ComponentBase where TItem : WsSqlTableBase, new()
{
    #region Public and private fields, properties, constructor
    [Parameter] public EventCallback OnItemUpdate { get; set; }
    [Parameter] public string Title { get; set; }

    #endregion
}
