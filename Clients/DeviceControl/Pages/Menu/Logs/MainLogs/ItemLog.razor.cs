namespace DeviceControl.Pages.Menu.Logs.MainLogs;

public sealed partial class ItemLog : ItemBase<WsSqlLogEntity>
{
    #region Public and private fields, properties, constructor

    private WsSqlLineRepository LineRepository { get; } = new();

    public ItemLog() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }

    #endregion
}
