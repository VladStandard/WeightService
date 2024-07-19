using Ws.Database.EntityFramework.Entities.Ref.PalletMen;
using Ws.DeviceControl.Api.App.Features.Admins.PalletMen.Common;
using Ws.DeviceControl.Api.App.Features.Admins.PalletMen.Impl.Expressions;
using Ws.DeviceControl.Models.Dto.Admins.PalletMen.Queries;

namespace Ws.DeviceControl.Api.App.Features.Admins.PalletMen.Impl;

public class PalletManApiService(WsDbContext dbContext) : IPalletManService
{
    #region Queries

    public Task<List<PalletManDto>> GetAllByProductionSiteAsync(Guid productionSiteId)
    {
        return dbContext.PalletMen
            .AsNoTracking()
            .Where(i => i.Warehouse.ProductionSite.Id == productionSiteId)
            .Select(PalletManExpressions.ToDto)
            .ToListAsync();
    }

    public async Task<PalletManDto> GetByIdAsync(Guid id)
    {
        PalletManEntity? palletMan = await dbContext.PalletMen.FindAsync(id);
        if (palletMan == null) throw new KeyNotFoundException();
        return PalletManExpressions.ToDto.Compile().Invoke(palletMan);
    }

    #endregion
}