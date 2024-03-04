using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.StorageMethod;

public interface IStorageMethodService : IGetItemByUid<StorageMethodEntity>, IGetAll<StorageMethodEntity>,
    ICreate<StorageMethodEntity>, IUpdate<StorageMethodEntity>, IDelete<StorageMethodEntity>
{
    StorageMethodEntity GetByName(string name);
    StorageMethodEntity GetDefault();
    StorageMethodEntity GetByNameOrDefault(string name);
}