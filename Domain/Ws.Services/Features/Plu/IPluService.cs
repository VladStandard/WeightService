using Ws.StorageCore.Entities.SchemaRef.PlusLines;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;
using Ws.StorageCore.Entities.SchemaScale.PlusNestingFks;
using Ws.StorageCore.Entities.SchemaScale.Templates;

namespace Ws.Services.Features.Plu;

public interface IPluService
{
    public IEnumerable<SqlPluNestingFkEntity> GetPluNesting(SqlPluEntity plu);
    public SqlTemplateEntity GetPluTemplate(SqlPluEntity plu);
    public SqlPluLineEntity GetPluLineByPlu1СAndLineName(Guid pluUid, string lineName);
}