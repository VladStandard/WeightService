using NHibernate.Criterion;
using Ws.Database.Core.Entities.Ref1c.Plus;
using Ws.Database.Core.Entities.Scales.PlusNestingFks;
using Ws.Database.Core.Entities.Scales.PlusTemplatesFks;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Plu;

public class PluService(SqlPluRepository pluRepo) : IPluService
{
    #region Queries

    [Session] public PluEntity GetItemByUid(Guid uid) => pluRepo.GetByUid(uid);

    [Session] public PluEntity GetItemByUid1С(Guid uid) => pluRepo.GetByUid1C(uid);

    [Session] public IEnumerable<PluEntity> GetAll() => pluRepo.GetAll();

    [Session] public IEnumerable<PluNestingEntity> GetAllPluNestings(PluEntity plu) =>
        new SqlPluNestingFkRepository().GetEnumerableByPlu(plu);

    [Session] public PluNestingEntity GetDefaultNesting(PluEntity plu) => new SqlPluNestingFkRepository().GetDefaultByPlu(plu);

    [Session]  public PluNestingEntity GetNestingByUid1C(PluEntity plu, Guid nestingUid1C) => 
        new SqlPluNestingFkRepository().GetByPluAndUid1C(plu, nestingUid1C);
    
    [Session] public TemplateEntity GetPluTemplate(PluEntity plu)
    {
        return new SqlPluTemplateFkRepository().GetItemByQuery(
        QueryOver.Of<PluTemplateFkEntity>().Where(i => i.Plu == plu)
        ).Template;
    }
    
    #endregion

    #region Commands

    [Session] public void DeleteAllPluNestings(PluEntity plu) => new SqlPluNestingFkRepository().DeleteAllPluNestings(plu);

    [Session] public void DeleteNestingByUid1C(PluEntity plu, Guid nestingUid1C)
    {
        PluNestingEntity nesting = GetNestingByUid1C(plu, nestingUid1C);
        if (nesting.IsExists) SqlCoreHelper.Delete(nesting);
    }

    #endregion
}