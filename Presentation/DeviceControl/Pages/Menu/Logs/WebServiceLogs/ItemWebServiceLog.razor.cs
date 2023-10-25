namespace DeviceControl.Pages.Menu.Logs.WebServiceLogs;

public sealed partial class ItemWebServiceLog : ItemBase<WsSqlLogWebEntity>
{
    #region Public and private fields, properties, constructor

    public ItemWebServiceLog() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }

    #endregion
}
