using Ws.StorageCore.Entities.SchemaRef.Hosts;

namespace Ws.Services.Services.Host;

public interface IHostService
{
    SqlHostEntity GetCurrentHostOrCreate();
}