using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;
using Ws.StorageCore.Entities.Ref.Lines;
using Ws.StorageCore.Entities.Ref.PlusLines;
using Ws.StorageCore.Entities.Ref1c.Plus;
using Ws.StorageCore.Entities.Scales.PlusNestingFks;
using Ws.StorageCore.Entities.Scales.PlusTemplatesFks;

namespace Ws.Services.Features.Plu;

public class PluService : IPluService
{
    public IEnumerable<PluNestingEntity> GetPluNesting(PluEntity plu)
    {
        return new SqlPluNestingFkRepository().GetEnumerableByPluUidActual(plu);
    }
    
    public TemplateEntity GetPluTemplate(PluEntity plu)
    {
        return new SqlPluTemplateFkRepository().GetItemByPlu(plu).Template;
    }

    public PluLineEntity GetPluLineByPlu1СAndLineName(Guid pluGuid, string lineName)
    {
        LineEntity line = new SqlLineRepository().GetItemByName(lineName);
        PluEntity plu = new SqlPluRepository().GetItemByUid1C(pluGuid);
        return new SqlPluLineRepository().GetItemByLinePlu(line, plu);
    }
}