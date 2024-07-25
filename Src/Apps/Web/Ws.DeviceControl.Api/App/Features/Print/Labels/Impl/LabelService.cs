using Ws.DeviceControl.Api.App.Features.Print.Labels.Common;
using Ws.DeviceControl.Api.App.Features.Print.Labels.Impl.Expressions;
using Ws.DeviceControl.Models.Dto.Print.Labels;

namespace Ws.DeviceControl.Api.App.Features.Print.Labels.Impl;

public class LabelService(
    WsDbContext dbContext
    ): ApiService, ILabelService
{
    #region Queries

    public async Task<LabelDto> GetByIdAsync(Guid id) =>
        LabelExpressions.ToDto.Compile().Invoke(await dbContext.Labels.SafeGetById(id, "Не найдено"));

    public Task<List<LabelDto>> GetAllAsync() => dbContext.Labels
        .AsNoTracking().Select(LabelExpressions.ToDto)
        .OrderByDescending(i => i.CreateDt)
        .ToListAsync();

    #endregion
}