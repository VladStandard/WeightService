using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Common;

namespace Ws.Domain.Services.Features.Warehouse;

public interface IWarehouseService : IAll<WarehouseEntity>, IUid<WarehouseEntity>;