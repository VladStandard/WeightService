using Ws.Database.Core.Common.Commands;
using Ws.Database.Core.Common.Queries.Item;
using Ws.Database.Core.Common.Queries.List;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Core.Entities.Ref1c.Brands;

public sealed class SqlBrandRepository :  BaseRepository, 
    IGetItemByUid1C<BrandEntity>, IGetItemByUid<BrandEntity>, IGetAll<BrandEntity>, ISave<BrandEntity>
{
    public BrandEntity GetByUid(Guid uid) => Session.Get<BrandEntity>(uid) ?? new();
    
    public BrandEntity GetByUid1C(Guid uid1C) => 
        Session.Query<BrandEntity>().FirstOrDefault(i => i.Uid1C == uid1C) ?? new();
    
    public IEnumerable<BrandEntity> GetAll() => Session.Query<BrandEntity>().OrderBy(i => i.Name).ToList();

    public BrandEntity Save(BrandEntity item) { Session.Save(item); return item; }
}