using System.Net;
using FluentValidation.Results;
using Ws.Database.EntityFramework.Entities.Ref.ProductionSites;
using Ws.DeviceControl.Api.App.Features.References.ProductionSites.Common;
using Ws.DeviceControl.Api.App.Features.References.ProductionSites.Impl.Expressions;
using Ws.DeviceControl.Models.Dto.References.ProductionSites.Queries;
using Ws.DeviceControl.Models.Dto.References.ProductionSites.Commands.Create;
using Ws.DeviceControl.Models.Dto.References.ProductionSites.Commands.Update;
using Ws.Shared.Api.ApiException;

namespace Ws.DeviceControl.Api.App.Features.References.ProductionSites.Impl;

public class ProductionSiteApiService(
    WsDbContext dbContext,
    ProductionSiteCreateValidator createValidator,
    ProductionSiteUpdateValidator updateValidator
    ): IProductionSiteService
{
    #region Queries

    public async Task<ProductionSiteDto> GetByIdAsync(Guid id)
    {
        ProductionSiteEntity? site = await dbContext.ProductionSites.FindAsync(id);
        if (site == null) throw new KeyNotFoundException();
        return ProductionSiteExpressions.ToDto.Compile().Invoke(site);
    }

    public Task<List<ProductionSiteDto>> GetAllAsync() => dbContext.ProductionSites
        .AsNoTracking().Select(ProductionSiteExpressions.ToDto)
        .OrderBy(i => i.Name)
        .ToListAsync();

    #endregion

    #region Commands

    public async Task<ProductionSiteDto> CreateAsync(ProductionSiteCreateDto dto)
    {
        ValidationResult result = await createValidator.ValidateAsync(dto);
        if (result.IsValid == false)
            throw new ApiExceptionServer
            {
                ErrorDisplayMessage = result.Errors.FirstOrDefault()?.ToString() ?? string.Empty,
                ErrorInternalMessage = result.Errors.FirstOrDefault()?.ToString() ?? string.Empty,
                StatusCode = HttpStatusCode.UnprocessableEntity
            };

        bool isExist = await dbContext.ProductionSites.AnyAsync(i => i.Name == dto.Name);
        if (isExist)
            throw new ApiExceptionServer
            {
                ErrorDisplayMessage = "Ошибка уникальности",
                StatusCode = HttpStatusCode.UnprocessableEntity
            };

        ProductionSiteEntity item = new()
        {
            Name = dto.Name,
            Address = dto.Address,
        };
       await dbContext.ProductionSites.AddAsync(item);

       await dbContext.SaveChangesAsync();

       return ProductionSiteExpressions.ToDto.Compile().Invoke(item);
    }

    public async Task<ProductionSiteDto> UpdateAsync(Guid id, ProductionSiteUpdateDto dto)
    {
        ValidationResult result = await updateValidator.ValidateAsync(dto);
        if (result.IsValid == false)
            throw new ApiExceptionServer
            {
                ErrorDisplayMessage = result.Errors.FirstOrDefault()?.ToString() ?? string.Empty,
                ErrorInternalMessage = result.Errors.FirstOrDefault()?.ToString() ?? string.Empty,
                StatusCode = HttpStatusCode.UnprocessableEntity
            };

        bool isExist = await dbContext.ProductionSites.AnyAsync(i => i.Name == dto.Name && i.Id != id);
        if (isExist)
            throw new ApiExceptionServer
            {
                ErrorDisplayMessage = "Ошибка уникальности",
                StatusCode = HttpStatusCode.UnprocessableEntity
            };

        ProductionSiteEntity? productionSite = await dbContext.ProductionSites.FindAsync(id);
        if (productionSite != null)
        {
            productionSite.Name = dto.Name;
            productionSite.Address = dto.Address;
            await dbContext.SaveChangesAsync();
        }
        else
            productionSite = new();

        return ProductionSiteExpressions.ToDto.Compile().Invoke(productionSite);
    }

    #endregion
}