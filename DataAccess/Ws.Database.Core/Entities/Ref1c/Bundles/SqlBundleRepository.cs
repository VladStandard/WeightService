using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Core.Entities.Ref1c.Bundles;

public sealed class SqlBundleRepository :  BaseRepository, IGetItemByUid1C<BundleEntity>, IGetItemByUid<BundleEntity>, IGetAll<BundleEntity>
{
    public BundleEntity GetByUid(Guid uid) => SqlCoreHelper.GetItemById<BundleEntity>(uid);
    
    public BundleEntity GetByUid1C(Guid uid1C)
    {
        return SqlCoreHelper.GetItem(
            QueryOver.Of<BundleEntity>().Where(i => i.Uid1C == uid1C)
        );
    }
    
    public IEnumerable<BundleEntity> GetAll()
    {
        return SqlCoreHelper.GetEnumerable(
            QueryOver.Of<BundleEntity>().OrderBy(i => i.Weight).Asc.ThenBy(i => i.Name).Asc
        );
    }

}