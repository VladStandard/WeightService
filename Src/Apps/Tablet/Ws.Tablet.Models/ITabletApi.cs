using Ws.Tablet.Models.Api;

namespace Ws.Tablet.Models;

public interface ITabletApi :
    ITabletArmApi,
    ITabletPluApi,
    ITabletUserApi,
    ITabletPalletApi;