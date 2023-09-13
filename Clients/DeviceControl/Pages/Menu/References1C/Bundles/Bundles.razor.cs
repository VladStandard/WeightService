namespace DeviceControl.Pages.Menu.References1C.Bundles;

public sealed partial class Bundles : SectionBase<WsSqlBundleModel>
{
    #region Public and private fields, properties, constructor

    public Bundles() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStatic1CSection();
    }
    
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new WsSqlBundleRepository().GetEnumerable(SqlCrudConfigSection).ToList();
    }

    #endregion
}
