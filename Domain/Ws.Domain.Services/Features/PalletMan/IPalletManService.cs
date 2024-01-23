using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Common;

namespace Ws.Domain.Services.Features.PalletMan;

public interface IPalletManService : IUid<PalletManEntity>, IAll<PalletManEntity>;