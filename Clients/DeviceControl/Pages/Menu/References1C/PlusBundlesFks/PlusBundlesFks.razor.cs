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
        SqlSectionCast = new WsSqlPluBundleFkRepository().GetEnumerable(SqlCrudConfigSection).ToList();
    }

    #endregion
}
