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
        Guid warehouse = dbContext
            .Lines.Where(i => i.Id == userHelper.UserId)
            .Select(i => i.Warehouse.Id).Single();

        List<PalletMan> palletMen = dbContext.PalletMen
            .AsNoTracking()
            .Where(i => i.Warehouse.Id == warehouse)
            .Select(PalletManExpressions.ToDto)
            .ToList();
        return palletMen;
    }

    #endregion
}