using Ws.Desktop.Models.Features.PalletMen;

namespace Ws.Desktop.Api.App.Features.PalletMen.Common;

public interface IPalletManService
{
    #region Queries

    PalletMan GetByCode(string code);

    #endregion
}