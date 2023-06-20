// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DeviceControl.Components.Section;
using WsBlazorCore.Settings;
using WsStorageCore.ViewScaleModels;

namespace DeviceControl.Pages.Menu.Operations.PlusLabels;

public sealed partial class PlusLabels : SectionBase<WsSqlViewPluLabelModel>
{
    #region Public and private fields, properties, constructor

    public PlusLabels() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticSection();
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = ContextViewHelper.GetListViewPlusLabels(SqlCrudConfigSection);
    }

    #endregion\
}