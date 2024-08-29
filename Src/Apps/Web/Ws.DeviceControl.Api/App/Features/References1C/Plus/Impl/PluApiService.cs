using Ws.Database.EntityFramework.Entities.Ref1C.Plus;
using Ws.DeviceControl.Api.App.Features.References1C.Plus.Common;
using Ws.DeviceControl.Api.App.Features.References1C.Plus.Impl.Expressions;
using Ws.DeviceControl.Models.Features.References1C.Plus.Queries;

namespace Ws.DeviceControl.Api.App.Features.References1C.Plus.Impl;

public class PluApiService(WsDbContext dbContext) : IPluService
{
    #region Queries

    public async Task<PluDto> GetByIdAsync(Guid id)
    {
        PluEntity entity = await dbContext.Plus.SafeGetById(id, "Не найдено");
        await LoadDefaultForeignKeysAsync(entity);
        return PluExpressions.ToDto.Compile().Invoke(entity);
    }


    public Task<List<PluDto>> GetAllAsync()
    {
        return dbContext.Plus
            .AsNoTracking()
            .Select(PluExpressions.ToDto)
            .ToListAsync();
    }

    #endregion

    #region Private

    private async Task LoadDefaultForeignKeysAsync(PluEntity entity)
    {
        await dbContext.Entry(entity).Reference(e => e.Clip).LoadAsync();
        await dbContext.Entry(entity).Reference(e => e.Brand).LoadAsync();
        await dbContext.Entry(entity).Reference(e => e.Bundle).LoadAsync();
        await dbContext.Entry(entity).Reference(e => e.Template).LoadAsync();
    }

    #endregion
}