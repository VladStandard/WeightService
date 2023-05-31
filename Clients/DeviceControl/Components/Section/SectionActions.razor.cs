// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsBlazorCore.Settings;

namespace DeviceControl.Components.Section;

public partial class SectionActions : LayoutComponentBase
{
    #region Public and private fields, properties, constructor

    [Parameter] public WsSqlCrudConfigModel SqlCrudConfigSection { get; set; }
    [Parameter] public ButtonSettingsModel ButtonSettings { get; set; }
    [Parameter] public EventCallback OnSectionSave { get; set; }

    public string Width { get; set; }

    public SectionActions()
    {
        Width = "5%";
    }

    #endregion

    #region Public and private methods

    //

    #endregion
}