using Ws.Desktop.Models.Features.Pallets.Input;
using Ws.Desktop.Models.Features.Pallets.Output;

namespace Ws.Desktop.Api.App.Features.Pallets.Common;

public interface IPalletApiService
{
    #region Queries

    public List<PalletInfo> GetByNumber(string number);
    public List<LabelInfo> GetAllZplByPallet(Guid palletId);
    public List<PalletInfo> GetAllByDate(DateTime startTime, DateTime endTime);

    #endregion

    #region Commands

    public Task Delete(Guid id);
    public Task<PalletInfo> CreatePiecePallet(PalletPieceCreateDto dto);

    #endregion
}