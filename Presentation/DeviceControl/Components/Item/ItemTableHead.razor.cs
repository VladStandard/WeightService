namespace DeviceControl.Components.Item;

public partial class ItemTableHead : ComponentBase
{
    private CssStyleTableHeadModel TableHeadModel { get; set; }
    [Parameter] public List<int> HeadWidth { get; set; } = new();

    protected override void OnInitialized()
    {
        TableHeadModel = HeadWidth.Any() ? 
            new(HeadWidth) : new CssStyleTableHeadModel();
    }
}
