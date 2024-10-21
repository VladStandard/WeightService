using Ws.Database.Entities.Zpl.Templates;
using Ws.DeviceControl.Api.App.Features.References.Templates.Common;
using Ws.DeviceControl.Api.App.Features.References.Templates.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.References.Templates.Impl.Extensions;
using Ws.DeviceControl.Api.App.Features.References.Templates.Impl.Validators;
using Ws.DeviceControl.Models.Features.References.Template.Commands;
using Ws.DeviceControl.Models.Features.References.Template.Queries;
using Ws.DeviceControl.Models.Features.References.Template.Universal;

namespace Ws.DeviceControl.Api.App.Features.References.Templates.Impl;

internal sealed class TemplateApiService(
    WsDbContext dbContext,
    TemplateUpdateValidator updateValidator,
    BarcodeItemWrapperValidator barcodeItemWrapperValidator,
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

    public async Task<BarcodeItemWrapper> GetBarcodeTemplates(Guid id)
    {
        TemplateEntity entity = await dbContext.Templates.SafeGetById(id, "Не найдено");

        return new()
        {
            Top = entity.BarcodeTopBody.ToDto(),
            Bottom = entity.BarcodeBottomBody.ToDto(),
            Right = entity.BarcodeRightBody.ToDto()
        };
    }

    #endregion

    #region Commands

    public async Task<TemplateDto> UpdateAsync(Guid id, TemplateUpdateDto dto)
    {
        await ValidateAsync(dto, updateValidator);

        TemplateEntity entity = await dbContext.Templates.SafeGetById(id, "Не найдено");

        await dbContext.Templates.ThrowIfExistAsync(
            i => i.Name == dto.Name && i.IsWeight == entity.IsWeight && i.Id != id, "Ошибка уникальности");

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

    public async Task<BarcodeItemWrapper> UpdateBarcodeTemplates(Guid id, BarcodeItemWrapper barcodes)
    {
        await barcodeItemWrapperValidator.ValidateAsync(barcodes);

        TemplateEntity entity = await dbContext.Templates.SafeGetById(id, "Не найдено");

        entity.BarcodeTopBody = barcodes.Top.ToItem();
        entity.BarcodeRightBody = barcodes.Right.ToItem();
        entity.BarcodeBottomBody = barcodes.Bottom.ToItem();

        await dbContext.SaveChangesAsync();

        return new()
        {
            Top = entity.BarcodeTopBody.ToDto(),
            Bottom = entity.BarcodeBottomBody.ToDto(),
            Right = entity.BarcodeRightBody.ToDto()
        };
    }

    #endregion
}