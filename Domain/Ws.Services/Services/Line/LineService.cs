using Ws.StorageCore.Entities.SchemaRef.Hosts;
using Ws.StorageCore.Entities.SchemaScale.Scales;

namespace Ws.Services.Services.Line;

public class LineService : ILineService
{
    public SqlScaleEntity GetLineByHost(SqlHostEntity host)
    {
        return new SqlLineRepository().GetItemByHost(host);
    }
}