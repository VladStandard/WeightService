using Ws.Mobile.Models.Features.Warehouses;

namespace Ws.Mobile.Api.App.Features.Warehouses.Common;

public interface IWarehouseService
{
    #region Queries

    List<WarehouseDto> GetAll();

    #endregion
}