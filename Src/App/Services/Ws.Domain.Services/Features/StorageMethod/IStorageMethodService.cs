using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.StorageMethod;

public interface IStorageMethodService : IGetItemByUid<Models.Entities.Print.StorageMethod>, IGetAll<Models.Entities.Print.StorageMethod>,
    ICreate<Models.Entities.Print.StorageMethod>, IUpdate<Models.Entities.Print.StorageMethod>, IDelete<Models.Entities.Print.StorageMethod>
{
    Models.Entities.Print.StorageMethod GetByName(string name);
    string? GetStorageByNameFromCacheOrDb(string name);
}