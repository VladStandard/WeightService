using Ws.Desktop.Models.Api;

namespace Ws.Desktop.Models;

public interface IDesktopApi :
    IDesktopArmApi,
    IDesktopPalletApi,
    IDesktopPalletManApi,
    IDesktopPluPieceApi,
    IDesktopPluWeightApi;