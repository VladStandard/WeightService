using Ws.StorageCore.Entities.SchemaRef.Hosts;

namespace DeviceControl.Pages.Menu.Devices.Hosts;

public sealed partial class Hosts : SectionBase<SqlHostEntity>
{
    private SqlHostRepository HostRepository { get; } = new();

    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = HostRepository.GetEnumerable(SqlCrudConfigSection).ToList();
    }
}
