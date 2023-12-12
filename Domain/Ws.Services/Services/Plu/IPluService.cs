using Ws.StorageCore.Entities.SchemaRef1c.Plus;
using Ws.StorageCore.Entities.SchemaScale.PlusNestingFks;
using Ws.StorageCore.Entities.SchemaScale.PlusScales;
using Ws.StorageCore.Entities.SchemaScale.Templates;

namespace Ws.Services.Services.Plu;

public interface IPluService
{
    public IEnumerable<SqlPluNestingFkEntity> GetPluNesting(SqlPluEntity plu);
    public SqlTemplateEntity GetPluTemplate(SqlPluEntity plu);
    public SqlPluScaleEntity GetPluLineByPlu1сAndLineName(Guid pluUid, string lineName);
}