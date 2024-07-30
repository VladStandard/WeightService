using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Ws.Database.EntityFramework.Entities.Ref.ProductionSites;
using Ws.DeviceControl.Api.App.Features.References.ProductionSites.Common;
using Ws.DeviceControl.Api.App.Features.References.ProductionSites.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.References.ProductionSites.Impl.Extensions;
using Ws.DeviceControl.Api.App.Shared.Internal;
using Ws.DeviceControl.Models.Dto.References.ProductionSites.Commands.Create;
using Ws.DeviceControl.Models.Dto.References.ProductionSites.Commands.Update;
using Ws.DeviceControl.Models.Dto.References.ProductionSites.Queries;
using Ws.Shared.Api.ApiException;
using Ws.Shared.Constants;

namespace Ws.DeviceControl.Api.App.Features.References.ProductionSites.Impl;

public class ProductionSiteApiService(
    WsDbContext dbContext,
    ProductionSiteCreateValidator createValidator,
    ProductionSiteUpdateValidator updateValidator,
    UserManager userManager
    ): ApiService, IProductionSiteService
{
    #region Queries

    public async Task<ProxyDto> GetProxyByUser()
    {
        ProxyDto? data = await userManager.GetUserProductionSiteAsync();
        if (data == null) throw new ApiExceptionServer
        {
            ErrorDisplayMessage = "Площадка не установлена",
            StatusCode = HttpStatusCode.NotFound
        };
        return data;
    }

    public async Task<List<ProxyDto>> GetProxiesAsync()
    {
        bool seniorSupport = await userManager.ValidatePolicyAsync(PolicyEnum.SeniorSupport);
        if (seniorSupport)
        {
            List<ProxyDto> data = await dbContext.ProductionSites
                .AsNoTracking()
                .Select(ProductionSiteExpressions.ToProxy)
                .OrderBy(i => i.Name)
                .ToListAsync();

            bool developer = await userManager.ValidatePolicyAsync(PolicyEnum.Developer);

            if (!developer) data.RemoveAll(i => i.Id == BaseConsts.GuidMax);

            return data;
        }
        ProxyDto? userProductionSite = await userManager.GetUserProductionSiteAsync();
        if (userProductionSite == null)
            return [];
        return [userProductionSite];
    }

    public async Task<ProductionSiteDto> GetByIdAsync(Guid id) =>
        ProductionSiteExpressions.ToDto.Compile().Invoke(await dbContext.ProductionSites.SafeGetById(id, "Не найдено"));

    public Task<List<ProductionSiteDto>> GetAllAsync() => dbContext.ProductionSites
        .AsNoTracking().Select(ProductionSiteExpressions.ToDto)
        .OrderBy(i => i.Name)
        .ToListAsync();

    #endregion

    #region Commands

    public async Task<ProductionSiteDto> CreateAsync(ProductionSiteCreateDto dto)
    {
        await ValidateAsync(dto, createValidator);
        await dbContext.ProductionSites.SafeExistAsync(i => i.Name == dto.Name, "Ошибка уникальности");

        ProductionSiteEntity entity = dto.ToEntity();

        await dbContext.ProductionSites.AddAsync(entity);
        await dbContext.SaveChangesAsync();

       return ProductionSiteExpressions.ToDto.Compile().Invoke(entity);
    }

    public async Task<ProductionSiteDto> UpdateAsync(Guid id, ProductionSiteUpdateDto dto)
    {
        await ValidateAsync(dto, updateValidator);
        await dbContext.ProductionSites.SafeExistAsync(i => i.Name == dto.Name && i.Id != id, "Ошибка уникальности");

        ProductionSiteEntity entity = await dbContext.ProductionSites.SafeGetById(id, "Не найдено");
        dto.UpdateEntity(entity);
        await dbContext.SaveChangesAsync();

        return ProductionSiteExpressions.ToDto.Compile().Invoke(entity);
    }

    #endregion
}