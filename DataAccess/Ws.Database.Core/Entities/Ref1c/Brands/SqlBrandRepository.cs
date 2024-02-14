using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Core.Entities.Ref1c.Brands;

public sealed class SqlBrandRepository :  BaseRepository, 
    IGetItemByUid1C<BrandEntity>, IGetItemByUid<BrandEntity>, IGetAll<BrandEntity>, ISave<BrandEntity>
{
    public BrandEntity GetByUid(Guid uid) => SqlCoreHelper.GetItemById<BrandEntity>(uid);
    
    public BrandEntity GetByUid1C(Guid uid1C)
    {
        return SqlCoreHelper.GetItem(
            QueryOver.Of<BrandEntity>().Where(i => i.Uid1C == uid1C)
        );
    }
    
    public IEnumerable<BrandEntity> GetAll()
    {
        return SqlCoreHelper.GetEnumerable(
            QueryOver.Of<BrandEntity>().OrderBy(i => i.Name).Asc
        );
    }
    
    public BrandEntity Save(BrandEntity item) => SqlCoreHelper.Save(item);
}