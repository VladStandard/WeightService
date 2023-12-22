namespace DeviceControl.Pages.Menu.Logs.WebServiceLogs;

public sealed partial class WebServiceLogs : SectionBase<SqlLogWebEntity>
{
    public WebServiceLogs() : base()
    {
        IsGuiShowFilterMarked = false;
        SqlCrudConfigSection.IsResultOrder = true;
        ButtonSettings = ButtonSettingsModel.CreateForStatic1CSection();
        
    }

    protected override void SetSqlSectionCast()
    {
        SqlCrudConfigModel crud = SqlCrudConfigFactory.GetCrudAll();
        SqlSectionCast = new SqlLogWebRepository().GetList(crud).ToList();
    }
}
