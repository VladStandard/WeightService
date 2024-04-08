using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Commands;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;

namespace Ws.Database.Nhibernate.Entities.Scales.PlusNestingFks;

public sealed class SqlPluNestingFkRepository : BaseRepository, IDelete<PluNestingEntity>
{
    public IEnumerable<PluNestingEntity> GetAllByPlu(PluEntity plu) =>
        Session.Query<PluNestingEntity>().Where(i => i.Plu == plu).ToList();

    public void DeleteAllPluNestings(PluEntity plu)
    {
        if (plu.IsNew) return;
        List<PluNestingEntity> pluNestingEntities = GetAllByPlu(plu).ToList();
        foreach (PluNestingEntity entity in pluNestingEntities)
            Session.Delete(entity);
    }

    public void Delete(PluNestingEntity item) => Session.Delete(item);
}