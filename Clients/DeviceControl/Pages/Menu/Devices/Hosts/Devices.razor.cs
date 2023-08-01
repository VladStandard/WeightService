// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DeviceControl.Pages.Menu.Devices.Hosts;

public sealed partial class Devices : SectionBase<WsSqlViewDeviceModel>
{
    #region Public and private methods

    private WsSqlViewDeviceRepository ViewDeviceRepository { get; } = new();
    
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = ViewDeviceRepository.GetList(SqlCrudConfigSection);
    }

    #endregion
}