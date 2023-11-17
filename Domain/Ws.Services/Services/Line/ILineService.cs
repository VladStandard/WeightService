using Ws.StorageCore.Entities.SchemaRef.Hosts;
using Ws.StorageCore.Entities.SchemaScale.Scales;

namespace Ws.Services.Services.Line;

public interface ILineService
{
    SqlScaleEntity GetLineByHost(SqlHostEntity host);
}