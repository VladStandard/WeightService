using Ws.Database.EntityFramework.Entities.Print.Labels;
using Ws.DeviceControl.Api.App.Features.Print.Labels.Common;
using Ws.DeviceControl.Api.App.Features.Print.Labels.Impl.Expressions;
using Ws.DeviceControl.Models.Dto.Print.Labels;

namespace Ws.DeviceControl.Api.App.Features.Print.Labels.Impl;

public class LabelApiService(
    WsDbContext dbContext
    ): ApiService, ILabelService
{
    #region Queries

    public async Task<LabelDto> GetByIdAsync(Guid id)
    {
        LabelEntity entity = await dbContext.Labels.SafeGetById(id, "Не найдено");
        await LoadDefaultForeignKeysAsync(entity);
        return LabelExpressions.ToLabelDto.Compile().Invoke(entity);
    }

    public Task<List<LabelDto>> GetAllAsync() => dbContext.Labels
        .AsNoTracking().Select(LabelExpressions.ToLabelDto)
        .OrderByDescending(i => i.CreateDt)
        .ToListAsync();

    public async Task<ZplDto> GetZplByIdAsync(Guid id) =>
        LabelExpressions.ToZplDto.Compile().Invoke(await dbContext.LabelZpl.SafeGetById(id, "Не найдено"));

    #endregion

    #region Private

    private async Task LoadDefaultForeignKeysAsync(LabelEntity entity)
    {
        await dbContext.Entry(entity).Reference(e => e.Plu).LoadAsync();
        await dbContext.Entry(entity).Reference(e => e.Line).LoadAsync();
        await dbContext.Entry(entity).Reference(e => e.Pallet).LoadAsync();
        await dbContext.Entry(entity.Line).Reference(e => e.Warehouse).LoadAsync();
        await dbContext.Entry(entity.Line.Warehouse).Reference(e => e.ProductionSite).LoadAsync();
    }

    #endregion
}