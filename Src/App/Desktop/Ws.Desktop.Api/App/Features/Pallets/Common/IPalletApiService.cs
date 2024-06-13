using Ws.Desktop.Models.Features.Pallets.Input;
using Ws.Desktop.Models.Features.Pallets.Output;

namespace Ws.Desktop.Api.App.Features.Pallets.Common;

public interface IPalletApiService
{
    public List<PalletInfo> GetAllByArm(Guid armId);
    public PalletInfo CreatePiecePallet(Guid armId, PalletPieceCreateDto dto);
}