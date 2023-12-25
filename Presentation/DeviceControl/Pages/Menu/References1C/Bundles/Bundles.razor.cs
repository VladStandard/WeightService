namespace DeviceControl.Pages.Menu.References1C.Bundles;

public sealed partial class Bundles : SectionBase<SqlBundleEntity>
{
    public Bundles() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStatic1CSection();
    }
    
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new SqlBundleRepository().GetEnumerable(SqlCrudConfigSection).ToList();
    }
}
