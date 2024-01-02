using Ws.StorageCore.Entities.SchemaRef.Hosts;
using Ws.StorageCore.Entities.SchemaRef.Lines;

namespace Ws.Services.Services.Host;

public interface IHostService
{
    SqlHostEntity GetCurrentHostOrCreate();
    SqlLineEntity GetLineByHost(SqlHostEntity host);
}