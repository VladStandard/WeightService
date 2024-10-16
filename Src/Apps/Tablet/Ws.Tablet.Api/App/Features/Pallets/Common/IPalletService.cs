using Ws.Tablet.Models.Features.Pallets.Input;
using Ws.Tablet.Models.Features.Pallets.Output;

namespace Ws.Tablet.Api.App.Features.Pallets.Common;

public interface IPalletService
{
    #region Commands

    PalletDto Create(PalletCreateDto palletCreateDto);

    #endregion
}