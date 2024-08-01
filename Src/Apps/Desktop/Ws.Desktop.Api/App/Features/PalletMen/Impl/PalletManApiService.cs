using Microsoft.EntityFrameworkCore;
using Ws.Database.EntityFramework;
using Ws.Desktop.Api.App.Features.PalletMen.Common;
using Ws.Desktop.Api.App.Features.PalletMen.Expressions;
using Ws.Desktop.Models.Features.PalletMen;

namespace Ws.Desktop.Api.App.Features.PalletMen.Impl;

public class PalletManApiService(WsDbContext dbContext) : IPalletManService
{
    #region Queries

    public List<PalletMan> GetAllByArm(Guid armId)
    {
        Guid warehouse = dbContext.Lines.Where(i => i.Id == armId).Select(i => i.Warehouse.Id).Single();
        List<PalletMan> palletMen = dbContext.PalletMen
            .AsNoTracking()
            .Where(i => i.Warehouse.Id == warehouse)
            .Select(PalletManExpressions.ToDto)
            .ToList();
        return palletMen;
    }

    #endregion
}