using Ws.Desktop.Models.Features.Pallets.Input;
using Ws.Desktop.Models.Features.Pallets.Output;

namespace Ws.Desktop.Api.App.Features.Pallets.Common;

public interface IPalletApiService
{
    public List<PalletInfo> GetAllByDate(Guid armId, DateTime startTime, DateTime endTime);
    public List<LabelInfo> GetAllZplByArm(Guid armId, Guid palletId);
    public PalletInfo CreatePiecePallet(Guid armId, PalletPieceCreateDto dto);
    public PalletInfo GetByNumber(Guid armId, uint number);
}