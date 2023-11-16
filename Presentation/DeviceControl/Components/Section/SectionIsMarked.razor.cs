namespace DeviceControl.Components.Section;

public partial class SectionIsMarked<TItem> : ComponentBase where TItem : SqlEntityBase, new()
{
    #region Public and private fields, properties, constructor

    [Parameter] public SqlCrudConfigModel SqlCrudConfigSection { get; set; }

    public string Width { get; set; }

    public SectionIsMarked()
    {
        Width = "5%";
    }

    #endregion
}
