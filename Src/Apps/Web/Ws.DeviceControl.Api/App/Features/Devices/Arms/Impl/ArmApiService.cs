using Ws.Database.EntityFramework.Entities.Ref.Lines;
using Ws.DeviceControl.Api.App.Features.Devices.Arms.Common;
using Ws.DeviceControl.Api.App.Features.Devices.Arms.Impl.Expressions;
using Ws.DeviceControl.Models.Dto.Devices.Arms.Queries;

namespace Ws.DeviceControl.Api.App.Features.Devices.Arms.Impl;

public class ArmApiService(WsDbContext dbContext) : IArmService
{
    #region Queries

    public Task<List<ArmDto>> GetAllByProductionSiteAsync(Guid productionSiteId)
    {
        return dbContext.Lines
            .AsNoTracking()
            .Where(i => i.Warehouse.ProductionSite.Id == productionSiteId)
            .Select(ArmExpressions.ToDto)
            .OrderBy(i => i.Name)
            .ToListAsync();
    }

    public async Task<ArmDto> GetByIdAsync(Guid id)
    {
        LineEntity? line = await dbContext.Lines.FindAsync(id);
        if (line == null) throw new KeyNotFoundException();
        return ArmExpressions.ToDto.Compile().Invoke(line);
    }

    #endregion
}