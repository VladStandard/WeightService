namespace DeviceControl.Pages.Menu.References1C.Plus;

public sealed partial class Plu : SectionBase<WsSqlPluModel>
{
    #region Public and private fields, properties, constructor

    public Plu() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticSection();
    }
    
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new WsSqlPluRepository().GetEnumerable(SqlCrudConfigSection).ToList();
    }

    #endregion
}
