using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.StorageMethods;

public interface IStorageMethodService : IGetItemByUid<StorageMethod>, IGetAll<StorageMethod>,
    ICreate<StorageMethod>, IUpdate<StorageMethod>, IDelete<StorageMethod>
{
    StorageMethod GetByName(string name);
}