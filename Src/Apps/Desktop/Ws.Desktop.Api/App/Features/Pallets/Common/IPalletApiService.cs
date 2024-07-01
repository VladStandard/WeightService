using Ws.Desktop.Models.Features.Pallets.Input;
using Ws.Desktop.Models.Features.Pallets.Output;

namespace Ws.Desktop.Api.App.Features.Pallets.Common;

public interface IPalletApiService
{
    #region Queries

    public List<PalletInfo> GetByNumber(Guid armId, uint number);
    public List<LabelInfo> GetAllZplByArm(Guid armId, Guid palletId);
    public List<PalletInfo> GetAllByDate(Guid armId, DateTime startTime, DateTime endTime);

    #endregion

    #region Commands

    public Task<PalletInfo> CreatePiecePallet(Guid armId, PalletPieceCreateDto dto);

    #endregion
}