// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DeviceControl.Pages.Menu.References1C.PlusBundlesFks;

public sealed partial class PlusBundlesFks : SectionBase<WsSqlPluBundleFkModel>
{
    #region Public and private fields, properties, constructor

    public PlusBundlesFks() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticSection();
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new WsSqlPluBundleFkRepository().GetList(SqlCrudConfigSection);
    }

    #endregion
}