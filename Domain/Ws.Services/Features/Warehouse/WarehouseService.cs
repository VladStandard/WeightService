using Ws.Database.Core.Entities.Ref.Warehouses;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Services.Features.Warehouse;

internal class WarehouseService : IWarehouseService
{
    public IEnumerable<WarehouseEntity> GetAll() => new SqlWarehouseRepository().GetEnumerable();
    public WarehouseEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<WarehouseEntity>(uid);
}