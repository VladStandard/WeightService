namespace DeviceControl.Components.Item;

public partial class ItemDates : ComponentBase
{
    #region Public and private fields, properties, constructor
    
    [Parameter] public WsSqlTableBase SqlItem { get; set; }

    #endregion
}
