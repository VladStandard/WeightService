using Microsoft.EntityFrameworkCore;
using Ws.Database;
using Ws.Desktop.Api.App.Features.PalletMen.Common;
using Ws.Desktop.Api.App.Features.PalletMen.Expressions;
using Ws.Desktop.Models.Features.PalletMen;
using Ws.Shared.Exceptions;

namespace Ws.Desktop.Api.App.Features.PalletMen.Impl;

internal sealed class PalletManApiService(WsDbContext dbContext, UserHelper userHelper) : IPalletManService
{
    #region Queries

    public PalletMan GetByCode(string code)
    {
        return dbContext.PalletMen
            .AsNoTracking()
            .Where(i => i.Warehouse.Id == userHelper.WarehouseId)
            .Select(PalletManExpressions.ToDto).FirstOrDefault() ?? throw new ApiInternalException
        {
            ErrorDisplayMessage = "Пользователь не найден",
            StatusCode = HttpStatusCode.NotFound
        };
    }

    #endregion
}