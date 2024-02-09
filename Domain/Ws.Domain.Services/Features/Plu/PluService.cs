using NHibernate.Criterion;
using Ws.Database.Core.Entities.Ref1c.Plus;
using Ws.Database.Core.Entities.Scales.PlusNestingFks;
using Ws.Database.Core.Entities.Scales.PlusTemplatesFks;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;

namespace Ws.Domain.Services.Features.Plu;

public class PluService : IPluService
{
    #region Queries

    public PluEntity GetItemByUid(Guid uid) => new SqlPluRepository().GetByUid(uid);

    public PluEntity GetItemByUid1С(Guid uid) => new SqlPluRepository().GetByUid1C(uid);

    public IEnumerable<PluEntity> GetAll() => new SqlPluRepository().GetAll();

    public IEnumerable<PluNestingEntity> GetAllPluNestings(PluEntity plu) =>
        new SqlPluNestingFkRepository().GetEnumerableByPlu(plu);

    public PluNestingEntity GetDefaultNesting(PluEntity plu) => new SqlPluNestingFkRepository().GetDefaultByPlu(plu);

    public PluNestingEntity GetNestingByUid1C(PluEntity plu, Guid nestingUid1C) => 
        new SqlPluNestingFkRepository().GetByPluAndUid1C(plu, nestingUid1C);
    
    public TemplateEntity GetPluTemplate(PluEntity plu)
    {
        return new SqlPluTemplateFkRepository().GetItemByQuery(
        QueryOver.Of<PluTemplateFkEntity>().Where(i => i.Plu == plu)
        ).Template;
    }
    
    #endregion

    #region Commands

    public void DeleteAllPluNestings(PluEntity plu) => new SqlPluNestingFkRepository().DeleteAllPluNestings(plu);

    public void DeleteNestingByUid1C(PluEntity plu, Guid nestingUid1C)
    {
        PluNestingEntity nesting = GetNestingByUid1C(plu, nestingUid1C);
        if (nesting.IsExists) SqlCoreHelper.Instance.Delete(nesting);
    }

    #endregion
}