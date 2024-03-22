using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.PalletMan;

public interface IPalletManService : IGetItemByUid<PalletManEntity>, IGetAll<PalletManEntity>,
    ICreate<PalletManEntity>, IUpdate<PalletManEntity>, IDelete<PalletManEntity>;