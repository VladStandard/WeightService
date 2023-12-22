namespace DeviceControl.Pages.Menu.References1C.Brands;

public sealed partial class Brands : SectionBase<SqlBrandEntity>
{
    public Brands() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStatic1CSection();
    }
    
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new SqlBrandRepository().GetEnumerable(SqlCrudConfigSection).ToList();
    }
}
