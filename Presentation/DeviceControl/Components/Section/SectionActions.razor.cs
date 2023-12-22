namespace DeviceControl.Components.Section;

public partial class SectionActions : ComponentBase
{
    [Parameter] public SqlCrudConfigModel SqlCrudConfigSection { get; set; }
    [Parameter] public ButtonSettingsModel ButtonSettings { get; set; }
    [Parameter] public EventCallback OnSectionSave { get; set; }

    public string Width { get; set; }

    public SectionActions()
    {
        Width = "5%";
    }
}
