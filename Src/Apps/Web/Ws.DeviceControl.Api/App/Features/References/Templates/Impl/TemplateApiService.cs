using Ws.Database.EntityFramework.Entities.Ref1C.Plus;
using Ws.Database.EntityFramework.Entities.Zpl.Templates;
using Ws.DeviceControl.Api.App.Features.References.Templates.Common;
using Ws.DeviceControl.Api.App.Features.References.Templates.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.References.Templates.Impl.Extensions;
using Ws.DeviceControl.Models.Dto.References.Template.Commands.Create;
using Ws.DeviceControl.Models.Dto.References.Template.Commands.Update;
using Ws.DeviceControl.Models.Dto.References.Template.Queries;

namespace Ws.DeviceControl.Api.App.Features.References.Templates.Impl;

public class TemplateApiService(
    WsDbContext dbContext,
    TemplateUpdateValidator updateValidator,
    TemplateCreateValidator createValidator
    ): ApiService, ITemplateService
{
    #region Queries

    public async Task<List<ProxyDto>> GetProxiesByPluAsync(Guid pluId)
    {
        PluEntity pluEntity = await dbContext.Plus.SafeGetById(pluId, "Не найдено");
        bool isWeight = pluEntity.IsWeight;

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

    #endregion

    #region Commands

    public async Task<TemplateDto> UpdateAsync(Guid id, TemplateUpdateDto dto)
    {
        await ValidateAsync(dto, updateValidator);
        await dbContext.Templates.SafeExistAsync(i => i.Name == dto.Name && i.Id != id, "Ошибка уникальности");

        TemplateEntity entity = await dbContext.Templates.SafeGetById(id, "Не найдено");
        dto.UpdateEntity(entity);
        await dbContext.SaveChangesAsync();

        return TemplateExpressions.ToDto.Compile().Invoke(entity);
    }

    public async Task<TemplateDto> CreateAsync(TemplateCreateDto dto)
    {
        await ValidateAsync(dto, createValidator);
        await dbContext.Templates.SafeExistAsync(i => i.Name == dto.Name, "Ошибка уникальности");

        TemplateEntity entity = dto.ToEntity();

        await dbContext.Templates.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return TemplateExpressions.ToDto.Compile().Invoke(entity);
    }

    #endregion
}