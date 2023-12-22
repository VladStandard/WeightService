namespace DeviceControl.Pages.Menu.Logs.MainLogs;

public sealed partial class ItemLog : ItemBase<SqlLogEntity>
{
    private SqlLineRepository LineRepository { get; } = new();

    public ItemLog() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }
}
