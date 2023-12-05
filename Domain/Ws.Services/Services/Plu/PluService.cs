using Ws.StorageCore.Entities.SchemaRef1c.Plus;
using Ws.StorageCore.Entities.SchemaScale.PlusNestingFks;
using Ws.StorageCore.Entities.SchemaScale.PlusTemplatesFks;
using Ws.StorageCore.Entities.SchemaScale.Templates;

namespace Ws.Services.Services.Plu;

public class PluService : IPluService
{
    public IEnumerable<SqlPluNestingFkEntity> GetPluNesting(SqlPluEntity plu)
    {
        return new SqlPluNestingFkRepository().GetEnumerableByPluUidActual(plu);
    }
    
    public SqlTemplateEntity GetPluTemplate(SqlPluEntity plu)
    {
        return new SqlPluTemplateFkRepository().GetItemByPlu(plu).Template;
    }
}