using Ws.Domain.Models.Entities.Ref;
using Ws.StorageCore.Entities.Ref.StorageMethods;
using Ws.StorageCore.Helpers;

namespace Ws.Services.Features.StorageMethod;

internal class StorageMethodService : IStorageMethodService
{
    public StorageMethodEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<StorageMethodEntity>(uid);

    public IEnumerable<StorageMethodEntity> GetAll() => new SqlStorageMethodRepository().GetList();
}