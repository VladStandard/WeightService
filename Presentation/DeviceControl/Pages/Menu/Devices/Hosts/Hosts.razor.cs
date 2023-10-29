using WsStorageCore.Entities.SchemaRef.Hosts;
namespace DeviceControl.Pages.Menu.Devices.Hosts;

public sealed partial class Hosts : SectionBase<WsSqlHostEntity>
{
    #region Public and private methods

    private WsSqlHostRepository HostRepository { get; } = new();

    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = HostRepository.GetEnumerable(SqlCrudConfigSection).ToList();
    }

    #endregion
}
