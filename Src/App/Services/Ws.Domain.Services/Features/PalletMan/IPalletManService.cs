using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.PalletMan;

public interface IPalletManService : IGetItemByUid<Models.Entities.Users.PalletMan>, IGetAll<Models.Entities.Users.PalletMan>,
    ICreate<Models.Entities.Users.PalletMan>, IUpdate<Models.Entities.Users.PalletMan>, IDelete<Models.Entities.Users.PalletMan>;