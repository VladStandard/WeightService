using Ws.StorageCore.Entities.SchemaRef1c.Plus;
using Ws.StorageCore.Entities.SchemaScale.PlusNestingFks;

namespace Ws.Services.Services.Plu;

public interface IPluService
{
    public IEnumerable<SqlPluNestingFkEntity> GetPluNesting(SqlPluEntity plu);
}