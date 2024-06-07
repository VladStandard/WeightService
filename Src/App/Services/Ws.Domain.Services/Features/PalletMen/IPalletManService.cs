using Ws.Domain.Models.Entities.Users;
using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.PalletMen;

public interface IPalletManService : IGetItemByUid<PalletMan>, IGetAll<PalletMan>,
    ICreate<PalletMan>, IUpdate<PalletMan>, IDelete<PalletMan>;