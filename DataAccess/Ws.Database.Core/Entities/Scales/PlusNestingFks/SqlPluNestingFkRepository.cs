using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;

namespace Ws.Database.Core.Entities.Scales.PlusNestingFks;

public sealed class SqlPluNestingFkRepository :  BaseRepository, IGetListByQuery<PluNestingEntity>
{
    public IEnumerable<PluNestingEntity> GetListByQuery(QueryOver<PluNestingEntity> query)
    {
        QueryOver<PluNestingEntity> queryOver = 
            query.Clone().JoinQueryOver<PluEntity>(i => i.Plu).OrderBy(plu => plu.Number).Asc;
        return SqlCoreHelper.GetEnumerable(queryOver);
    }
    
    public IEnumerable<PluNestingEntity> GetEnumerableByPlu(PluEntity plu) =>
        GetListByQuery(QueryOver.Of<PluNestingEntity>().Where(i => i.Plu == plu));

    public PluNestingEntity GetByPluAndUid1C(PluEntity plu, Guid uid1C)
    {
        return SqlCoreHelper.GetItem(
            QueryOver.Of<PluNestingEntity>().Where(i => i.Plu == plu && i.Uid1C == uid1C)
        );
    }
    
    public PluNestingEntity GetDefaultByPlu(PluEntity plu)
    {
        return SqlCoreHelper.GetItem(
            QueryOver.Of<PluNestingEntity>().Where(i => i.Plu == plu && i.Uid1C == Guid.Empty)
        );
    }

    public void DeleteAllPluNestings(PluEntity plu)
    {
        if (plu.IsNew) return;
        List<PluNestingEntity> pluNestingEntities = GetEnumerableByPlu(plu).ToList();
        foreach (PluNestingEntity entity in pluNestingEntities)
            SqlCoreHelper.Delete(entity);
    }
}