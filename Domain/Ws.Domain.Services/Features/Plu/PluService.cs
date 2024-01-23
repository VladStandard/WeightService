using Ws.Database.Core.Entities.Ref.Lines;
using Ws.Database.Core.Entities.Ref.PlusLines;
using Ws.Database.Core.Entities.Ref1c.Plus;
using Ws.Database.Core.Entities.Scales.PlusFks;
using Ws.Database.Core.Entities.Scales.PlusNestingFks;
using Ws.Database.Core.Entities.Scales.PlusTemplatesFks;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;

namespace Ws.Domain.Services.Features.Plu;

public class PluService : IPluService
{
    public PluEntity GetByUid(Guid uid) => new SqlPluRepository().GetByUid(uid);
    
    public PluEntity GetByUid1С(Guid uid) => new SqlPluRepository().GetByUid1C(uid);
    
    public IEnumerable<PluEntity> GetAllNotGroup() => new SqlPluRepository().GetEnumerableNotGroup();
    
    public IEnumerable<PluNestingEntity> GetPluNestings(PluEntity plu) =>
        new SqlPluNestingFkRepository().GetEnumerableByPluUidActual(plu);

    public PluNestingEntity GetDefaultNesting(PluEntity plu) =>
        new SqlPluNestingFkRepository().GetDefaultByPlu(plu);
    
    public PluNestingEntity GetNestingByUid1C(PluEntity plu, Guid nestingUid1C) =>
        new SqlPluNestingFkRepository().GetByPluAndUid1C(plu, nestingUid1C);

    public void DeleteNestingByUid1C(PluEntity plu, Guid nestingUid1C)
    {
        PluNestingEntity nesting = GetNestingByUid1C(plu, nestingUid1C);
        if (nesting.IsExists) SqlCoreHelper.Instance.Delete(nesting);
    }
    
    public TemplateEntity GetPluTemplate(PluEntity plu) => 
        new SqlPluTemplateFkRepository().GetItemByPlu(plu).Template;
    
    public PluLineEntity GetPluLineByPlu1СAndLineName(Guid pluGuid, string lineName)
    {
        LineEntity line = new SqlLineRepository().GetItemByName(lineName);
        PluEntity plu = new SqlPluRepository().GetByUid1C(pluGuid);
        return new SqlPluLineRepository().GetItemByLinePlu(line, plu);
    }

    public PluFkEntity GetParent(PluEntity plu) => new SqlPluFkRepository().GetByPlu(plu);

    public IEnumerable<PluEntity> GetInRange(List<Guid> uniquePluGuids) =>
        new SqlPluRepository().GetPluUid1CInRange(uniquePluGuids);
}