// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleFkModels.PlusBundlesFks;

namespace BlazorDeviceControl.Pages.Menu.References1C.SectionPlusBundlesFks;

public sealed partial class PlusBundlesFks : RazorComponentSectionBase<WsSqlPluBundleFkModel>
{
    #region Public and private fields, properties, constructor

    public PlusBundlesFks() : base()
    {
        SqlCrudConfigSection.IsGuiShowFilterAdditional = true;
        ButtonSettings = new(false, false, true, true, false, false, false);
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        SqlCrudConfigSection.AddFilters(nameof(WsSqlPluBundleFkModel.Plu), SqlItem);
        base.SetSqlSectionCast();
    }

    #endregion
}