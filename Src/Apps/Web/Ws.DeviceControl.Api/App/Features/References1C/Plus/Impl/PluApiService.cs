using Ws.DeviceControl.Api.App.Features.References1C.Plus.Common;
using Ws.DeviceControl.Api.App.Features.References1C.Plus.Impl.Expressions;
using Ws.DeviceControl.Api.App.Shared.Extensions;
using Ws.DeviceControl.Models.Dto.References1C.Plus.Queries;

namespace Ws.DeviceControl.Api.App.Features.References1C.Plus.Impl;

public class PluApiService(WsDbContext dbContext) : IPluService
{
    #region Queries

    public async Task<PluDto> GetByIdAsync(Guid id) =>
        PluExpressions.ToDto.Compile().Invoke(await dbContext.Plus.SafeGetById(id, "Не найдено"));

    public Task<List<PluDto>> GetAllAsync()
    {
        return dbContext.Plus
            .AsNoTracking()
            .Select(PluExpressions.ToDto)
            .ToListAsync();
    }

    #endregion
}