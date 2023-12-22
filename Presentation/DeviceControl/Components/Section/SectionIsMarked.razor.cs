namespace DeviceControl.Components.Section;

public partial class SectionIsMarked<TItem> : ComponentBase where TItem : SqlEntityBase, new()
{
    [Parameter] public SqlCrudConfigModel SqlCrudConfigSection { get; set; }

    public string Width { get; set; }

    public SectionIsMarked()
    {
        Width = "5%";
    }
}
