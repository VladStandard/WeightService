using Ws.Mobile.Api.App.Features.Warehouses.Common;
using Ws.Mobile.Models.Features.Warehouses;

namespace Ws.Mobile.Api.App.Features.Warehouses.Impl;

internal sealed class WarehouseApiService : IWarehouseService
{
    #region Queries

    public List<WarehouseDto> GetAll()
    {
        return
        [
            new()
            {
                Id = Guid.NewGuid(),
                ProductionSiteName = "Площадка камешково",
                WarehouseName = "Склад собаки",
            },
            new()
            {
                Id = Guid.NewGuid(),
                ProductionSiteName = "Площадка камешково",
                WarehouseName = "Склад кошек",
            },
            new()
            {
                Id = Guid.NewGuid(),
                ProductionSiteName = "Площадка камешково",
                WarehouseName = "Склад мамонтов",
            },
        ];
    }

    #endregion
}