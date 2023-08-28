namespace DeviceControl.Pages.Menu.Admins;

public sealed partial class Versions : SectionBase<WsSqlVersionModel>
{
    #region Public and private fields, properties, constructor

    public Versions() : base()
    {
        IsGuiShowFilterMarked = false;
        ButtonSettings = ButtonSettingsModel.CreateForStaticSection();
    }
    
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new WsSqlVersionRepository().GetList(SqlCrudConfigSection);
    }

    #endregion
}
