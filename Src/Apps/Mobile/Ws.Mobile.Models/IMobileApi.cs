using Ws.Mobile.Models.Api;

namespace Ws.Mobile.Models;

public interface IMobileApi :
    ITabletWarehouseApi,
    ITabletUserApi,
    ITabletPalletApi;