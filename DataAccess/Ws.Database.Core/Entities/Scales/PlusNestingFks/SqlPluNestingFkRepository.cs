using Ws.Database.Core.Common.Commands;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;

namespace Ws.Database.Core.Entities.Scales.PlusNestingFks;

public sealed class SqlPluNestingFkRepository : BaseRepository, IDelete<PluNestingEntity>
{
    public IEnumerable<PluNestingEntity> GetAllByPlu(PluEntity plu) =>
        Session.Query<PluNestingEntity>().Where(i => i.Plu == plu).ToList();

    public PluNestingEntity GetByPluAndUid1C(PluEntity plu, Guid uid1C) =>
        Session.Query<PluNestingEntity>()
            .FirstOrDefault(i => i.Plu == plu && i.Uid1C == uid1C) ?? new();

    public PluNestingEntity GetDefaultByPlu(PluEntity plu) =>
        Session.Query<PluNestingEntity>()
            .FirstOrDefault(i => i.Plu == plu && i.Uid1C == Guid.Empty) ?? new();

    public void DeleteAllPluNestings(PluEntity plu)
    {
        if (plu.IsNew) return;
        List<PluNestingEntity> pluNestingEntities = GetAllByPlu(plu).ToList();
        foreach (PluNestingEntity entity in pluNestingEntities)
            Session.Delete(entity);
    }
    
    public void Delete(PluNestingEntity item) => Session.Delete(item);
}