using Microsoft.EntityFrameworkCore;
using Ws.Database;
using Ws.Desktop.Api.App.Features.PalletMen.Common;
using Ws.Desktop.Api.App.Features.PalletMen.Expressions;
using Ws.Desktop.Models.Features.PalletMen;

namespace Ws.Desktop.Api.App.Features.PalletMen.Impl;

internal sealed class PalletManApiService(WsDbContext dbContext, UserHelper userHelper) : IPalletManService
{
    #region Queries

    public List<PalletMan> GetAll()
    {
        List<PalletMan> palletMen = dbContext.PalletMen
            .AsNoTracking()
            .Where(i => i.Warehouse.Id == userHelper.WarehouseId)
            .Select(PalletManExpressions.ToDto)
            .ToList();
        return palletMen;
    }

    #endregion
}