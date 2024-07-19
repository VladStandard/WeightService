using Ws.Database.EntityFramework.Entities.Ref1C.Plus;
using Ws.DeviceControl.Api.App.Features.References1C.Plus.Common;
using Ws.DeviceControl.Api.App.Features.References1C.Plus.Impl.Expressions;
using Ws.DeviceControl.Models.Dto.References1C.Plus.Queries;

namespace Ws.DeviceControl.Api.App.Features.References1C.Plus.Impl;

public class PluApiService(WsDbContext dbContext) : IPluService
{
    #region Queries

    public async Task<PluDto> GetByIdAsync(Guid id)
    {
        PluEntity? plu = await dbContext.Plus.FindAsync(id);
        if (plu == null) throw new KeyNotFoundException();
        return PluExpressions.ToDto.Compile().Invoke(plu);
    }

    public Task<List<PluDto>> GetAllAsync()
    {
        return dbContext.Plus
            .AsNoTracking()
            .Select(PluExpressions.ToDto)
            .ToListAsync();
    }

    #endregion
}