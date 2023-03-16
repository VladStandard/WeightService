// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;

namespace BlazorDeviceControl.Pages.SectionComponents.Plus;

public partial class SectionPlusBundlesFks : RazorComponentSectionBase<PluBundleFkModel>
{
    #region Public and private fields, properties, constructor

    public SectionPlusBundlesFks() : base()
    {
        SqlCrudConfigSection.IsGuiShowFilterAdditional = true;
        ButtonSettings = new(true, true, true, true, true, true, false);
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        SqlCrudConfigSection.AddFilters(nameof(PluBundleFkModel.Plu), SqlItem);
        base.SetSqlSectionCast();
    }

    #endregion
}
