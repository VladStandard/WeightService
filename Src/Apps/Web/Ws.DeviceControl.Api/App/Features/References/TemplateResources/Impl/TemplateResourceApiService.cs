using System.Net;
using Svg;
using Ws.Database.EntityFramework.Entities.Zpl.ZplResources;
using Ws.DeviceControl.Api.App.Features.References.TemplateResources.Common;
using Ws.DeviceControl.Api.App.Features.References.TemplateResources.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.References.TemplateResources.Impl.Extensions;
using Ws.DeviceControl.Models.Dto.References.TemplateResources.Commands.Create;
using Ws.DeviceControl.Models.Dto.References.TemplateResources.Commands.Update;
using Ws.DeviceControl.Models.Dto.References.TemplateResources.Queries;
using Ws.Shared.Api.ApiException;

namespace Ws.DeviceControl.Api.App.Features.References.TemplateResources.Impl;

public class TemplateResourceApiService(
    WsDbContext dbContext,
    TemplateResourceUpdateValidator updateValidator,
    TemplateResourceCreateValidator createValidator
    ): ApiService, ITemplateResourceService
{
    #region Queries

    public async Task<TemplateResourceDto> GetByIdAsync(Guid id) =>
        TemplateResourceExpressions.ToDto.Compile().Invoke(await dbContext.ZplResources.SafeGetById(id, "Не найдено"));

    public Task<List<TemplateResourceDto>> GetAllAsync() => dbContext.ZplResources
        .AsNoTracking().Select(TemplateResourceExpressions.ToDto)
        .OrderBy(i => i.Type)
        .ThenBy(i => i.Name)
        .ToListAsync();

    #endregion

    #region Commands

    public async Task<TemplateResourceBodyDto> GetBodyByIdAsync(Guid id)
    {
        ZplResourceEntity entity = await dbContext.ZplResources.SafeGetById(id, "Не найдено");
        return new()
        {
            Body = entity.Zpl
        };
    }

    public async Task<TemplateResourceDto> UpdateAsync(Guid id, TemplateResourceUpdateDto dto)
    {
        await ValidateAsync(dto, updateValidator);
        ValidateSvg(dto.Body);
        await dbContext.ZplResources.SafeExistAsync(i => i.Name == dto.Name && i.Id != id, "Ошибка уникальности");

        ZplResourceEntity entity = await dbContext.ZplResources.SafeGetById(id, "Не найдено");
        dto.UpdateEntity(entity);
        await dbContext.SaveChangesAsync();

        return TemplateResourceExpressions.ToDto.Compile().Invoke(entity);
    }

    public async Task<TemplateResourceDto> CreateAsync(TemplateResourceCreateDto dto)
    {
        await ValidateAsync(dto, createValidator);
        ValidateSvg(dto.Body);
        await dbContext.ZplResources.SafeExistAsync(i => i.Name == dto.Name, "Ошибка уникальности");

        ZplResourceEntity entity = dto.ToEntity();

        await dbContext.ZplResources.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return TemplateResourceExpressions.ToDto.Compile().Invoke(entity);
    }

    #endregion

    private static void ValidateSvg(string svg)
    {
        try
        {
            SvgDocument.FromSvg<SvgDocument>(svg);
        }
        catch
        {
            throw new ApiExceptionServer
            {
                ErrorDisplayMessage = "zpl error",
                StatusCode = HttpStatusCode.UnprocessableEntity
            };
        }
    }
}