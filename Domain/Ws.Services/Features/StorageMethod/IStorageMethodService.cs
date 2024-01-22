using Ws.Domain.Models.Entities.Ref;
using Ws.Services.Common;

namespace Ws.Services.Features.StorageMethod;

public interface IStorageMethodService : IUid<StorageMethodEntity>, IAll<StorageMethodEntity>
{
    StorageMethodEntity GetByName(string name);
    StorageMethodEntity GetDefault();
    StorageMethodEntity GetByNameOrDefault(string name);
}