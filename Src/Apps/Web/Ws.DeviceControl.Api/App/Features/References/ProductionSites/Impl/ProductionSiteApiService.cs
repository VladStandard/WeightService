using System.Security.Claims;
using Ws.Database.EntityFramework.Entities.Ref.ProductionSites;
using Ws.Database.EntityFramework.Entities.Ref.Users;
using Ws.DeviceControl.Api.App.Features.References.ProductionSites.Common;
using Ws.DeviceControl.Api.App.Features.References.ProductionSites.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.References.ProductionSites.Impl.Extensions;
using Ws.DeviceControl.Models.Dto.References.ProductionSites.Commands.Create;
using Ws.DeviceControl.Models.Dto.References.ProductionSites.Commands.Update;
using Ws.DeviceControl.Models.Dto.References.ProductionSites.Queries;

namespace Ws.DeviceControl.Api.App.Features.References.ProductionSites.Impl;

public class ProductionSiteApiService(
    WsDbContext dbContext,
    ProductionSiteCreateValidator createValidator,
    ProductionSiteUpdateValidator updateValidator,
    IHttpContextAccessor httpContextAccessor
    ): ApiService(), IProductionSiteService
{
    #region Queries

    public async Task<List<ProxyDto>> GetProxiesAsync()
    {
        List<ProxyDto> productionSites = [];

        UserEntity user = await dbContext.Users
            .Include(i => i.ProductionSite)
            .FirstAsync(i => i.Id == UserId);

        ProxyDto userProduction = new()
        {
            Id = user.ProductionSite.Id,
            Name = user.ProductionSite.Name
        };
        productionSites.Add(userProduction);

        if (User.HasClaim(ClaimTypes.Role, RoleEnum.Support))
        {
            List<ProxyDto> data = await dbContext.ProductionSites
                .AsNoTracking()
                .Select(ProductionSiteExpressions.ToProxy)
                .OrderBy(i => i.Name)
                .ToListAsync();
            return productionSites.Concat(data).ToList();
        }
        return productionSites;
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

    protected ClaimsPrincipal User => httpContextAccessor.HttpContext!.User;

    protected Guid UserId => Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid result)
        ? result : Guid.Empty;
}