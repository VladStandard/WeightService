namespace DeviceControl.Pages.Menu.Logs.WebServiceLogs;

public sealed partial class ItemWebServiceLog : ItemBase<SqlLogWebEntity>
{
    public ItemWebServiceLog() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }
}
