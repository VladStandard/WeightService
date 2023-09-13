namespace DeviceControl.Pages.Menu.References1C.Brands;

public sealed partial class Brands : SectionBase<WsSqlBrandModel>
{
    #region Public and private fields, properties, constructor

    public Brands() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStatic1CSection();
    }
    
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new WsSqlBrandRepository().GetEnumerable(SqlCrudConfigSection).ToList();
    }
    
    #endregion
}
