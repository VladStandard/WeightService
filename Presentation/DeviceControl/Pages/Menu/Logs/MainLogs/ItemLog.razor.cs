namespace DeviceControl.Pages.Menu.Logs.MainLogs;

public sealed partial class ItemLog : ItemBase<SqlLogEntity>
{
    #region Public and private fields, properties, constructor

    private SqlLineRepository LineRepository { get; } = new();

    public ItemLog() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }

    #endregion
}
