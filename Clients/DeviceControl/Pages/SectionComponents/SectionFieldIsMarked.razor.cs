// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Pages.SectionComponents;

public partial class SectionFieldIsMarked<TItem> : LayoutComponentBase where TItem : WsSqlTableBase, new()
{
    #region Public and private fields, properties, constructor

    [Parameter] public WsSqlCrudConfigModel SqlCrudConfigSection { get; set; }

    public string Width { get; set; }

    public SectionFieldIsMarked()
    {
        Width = "5%";
    }

    #endregion

    #region Public and private methods

    //

    #endregion
}
