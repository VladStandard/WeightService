using Ws.StorageCore.Entities.SchemaRef.Lines;
using Ws.StorageCore.Entities.SchemaRef.PlusLines;
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

    public SqlPluLineEntity GetPluLineByPlu1СAndLineName(Guid pluGuid, string lineName)
    {
        SqlLineEntity line = new SqlLineRepository().GetItemByName(lineName);
        SqlPluEntity plu = new SqlPluRepository().GetItemByUid1C(pluGuid);
        return new SqlPluLineRepository().GetItemByLinePlu(line, plu);
    }
}