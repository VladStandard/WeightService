namespace DeviceControl.Pages.Menu.Devices.Hosts;

public sealed partial class Devices : SectionBase<WsSqlViewDeviceModel>
{
    #region Public and private methods

    private WsSqlViewDeviceRepository ViewDeviceRepository { get; } = new();
    
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = ViewDeviceRepository.GetList(SqlCrudConfigSection).ToList();
    }

    #endregion
}
