using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Common;

namespace Ws.Domain.Services.Features.StorageMethod;

public interface IStorageMethodService : IUid<StorageMethodEntity>, IAll<StorageMethodEntity>
{
    StorageMethodEntity GetByName(string name);
    StorageMethodEntity GetDefault();
    StorageMethodEntity GetByNameOrDefault(string name);
}