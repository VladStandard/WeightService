using Ws.Domain.Models.Entities.Ref;
using Ws.StorageCore.Entities.Ref.Warehouses;
using Ws.StorageCore.Helpers;

namespace Ws.Services.Features.Warehouse;

internal class WarehouseService : IWarehouseService
{
    public IEnumerable<WarehouseEntity> GetAll() => new SqlWarehouseRepository().GetEnumerable();
    public WarehouseEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<WarehouseEntity>(uid);
}