using Ws.Database.EntityFramework.Entities.Zpl.Templates;
using Ws.DeviceControl.Api.App.Features.References.Templates.Common;
using Ws.DeviceControl.Api.App.Features.References.Templates.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.References.Templates.Impl.Extensions;
using Ws.DeviceControl.Api.App.Shared.Utils;
using Ws.DeviceControl.Models.Features.References.Template;
using Ws.DeviceControl.Models.Features.References.Template.Commands.Create;
using Ws.DeviceControl.Models.Features.References.Template.Commands.Update;
using Ws.DeviceControl.Models.Features.References.Template.Queries;

namespace Ws.DeviceControl.Api.App.Features.References.Templates.Impl;

internal sealed class TemplateApiService(
    WsDbContext dbContext,
    TemplateUpdateValidator updateValidator,
    TemplateCreateValidator createValidator
    ) : ApiService, ITemplateService
{
    #region Queries

    public async Task<List<ProxyDto>> GetProxiesByIsWeightAsync(bool isWeight)
    {
        return await dbContext.Templates
            .AsNoTracking()
            .Where(i => i.IsWeight == isWeight)
            .Select(TemplateExpressions.ToProxy)
            .OrderBy(i => i.Name)
            .ToListAsync();
    }

    public async Task<TemplateDto> GetByIdAsync(Guid id) =>
        TemplateExpressions.ToDto.Compile().Invoke(await dbContext.Templates.SafeGetById(id, "Не найдено"));

    public Task<List<TemplateDto>> GetAllAsync() => dbContext.Templates
        .AsNoTracking().Select(TemplateExpressions.ToDto)
        .OrderBy(i => i.IsWeight).ThenBy(i => i.Name)
        .ToListAsync();

    public async Task<TemplateBodyDto> GetBodyByIdAsync(Guid id)
    {
        TemplateEntity entity = await dbContext.Templates.SafeGetById(id, "Не найдено");
        return new()
        {
            Body = entity.Body
        };
    }

    public Task<List<BarcodeVar>> GetBarcodeVariables() => Task.FromResult(BarcodeUtils.GetVariables());

    #endregion

    #region Commands

    public async Task<TemplateDto> UpdateAsync(Guid id, TemplateUpdateDto dto)
    {
        await ValidateAsync(dto, updateValidator);
        await dbContext.Templates.ThrowIfExistAsync(i => i.Name == dto.Name && i.Id != id, "Ошибка уникальности");

        TemplateEntity entity = await dbContext.Templates.SafeGetById(id, "Не найдено");
        dto.UpdateEntity(entity);
        await dbContext.SaveChangesAsync();

        return TemplateExpressions.ToDto.Compile().Invoke(entity);
    }

    public async Task<TemplateDto> CreateAsync(TemplateCreateDto dto)
    {
        await ValidateAsync(dto, createValidator);
        await dbContext.Templates.ThrowIfExistAsync(i => i.Name == dto.Name, "Ошибка уникальности");

        TemplateEntity entity = dto.ToEntity();

        await dbContext.Templates.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return TemplateExpressions.ToDto.Compile().Invoke(entity);
    }

    public Task DeleteAsync(Guid id) => dbContext.Templates.SafeDeleteAsync(i => i.Id == id);

    #endregion
}