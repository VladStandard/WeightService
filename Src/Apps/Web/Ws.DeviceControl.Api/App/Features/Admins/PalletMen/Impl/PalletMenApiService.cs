using Ws.Database.EntityFramework.Entities.Ref.PalletMen;
using Ws.Database.EntityFramework.Entities.Ref.Warehouses;
using Ws.DeviceControl.Api.App.Features.Admins.PalletMen.Common;
using Ws.DeviceControl.Api.App.Features.Admins.PalletMen.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.Admins.PalletMen.Impl.Extensions;
using Ws.DeviceControl.Models.Dto.Admins.PalletMen.Commands.Create;
using Ws.DeviceControl.Models.Dto.Admins.PalletMen.Commands.Update;
using Ws.DeviceControl.Models.Dto.Admins.PalletMen.Queries;

namespace Ws.DeviceControl.Api.App.Features.Admins.PalletMen.Impl;

public class PalletManApiService(
    WsDbContext dbContext,
    PalletManCreateValidator createValidator,
    PalletManUpdateValidator updateValidator,
    UserManager userManager
    ) : ApiService, IPalletManService
{
    #region Queries

    public Task<List<PalletManDto>> GetAllByProductionSiteAsync(Guid productionSiteId)
    {
        return dbContext.PalletMen
            .AsNoTracking()
            .Where(i => i.Warehouse.ProductionSite.Id == productionSiteId)
            .Select(PalletManExpressions.ToDto)
            .ToListAsync();
    }

    public async Task<PalletManDto> GetByIdAsync(Guid id)
    {
        PalletManEntity palletMan = await dbContext.PalletMen.SafeGetById(id, "Не найдено");
        await LoadDefaultForeignKeysAsync(palletMan);
        return PalletManExpressions.ToDto.Compile().Invoke(palletMan);
    }

    #endregion

    #region Commands

    public async Task<PalletManDto> CreateAsync(PalletManCreateDto dto)
    {
        await ValidateAsync(dto, createValidator);
        await dbContext.PalletMen.SafeExistAsync(i => i.Name == dto.Name && i.Surname == dto.Surname && i.Patronymic == dto.Patronymic, "Ошибка уникальности");
        await dbContext.PalletMen.SafeExistAsync(i => i.Uid1C == dto.Id1C, "Ошибка уникальности");

        WarehouseEntity warehouse  = await dbContext.Warehouses.SafeGetById(dto.WarehouseId, "Не найдено");
        await userManager.CanUserWorkWithProductionSiteAsync(warehouse.ProductionSiteId);

        PalletManEntity entity = dto.ToEntity(warehouse);

        await dbContext.PalletMen.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        await LoadDefaultForeignKeysAsync(entity);
        return PalletManExpressions.ToDto.Compile().Invoke(entity);
    }

    public async Task<PalletManDto> UpdateAsync(Guid id, PalletManUpdateDto dto)
    {
        await ValidateAsync(dto, updateValidator);
        await dbContext.PalletMen.SafeExistAsync(i => i.Name == dto.Name && i.Surname == dto.Surname && i.Patronymic == dto.Patronymic && i.Id != id, "Ошибка уникальности");
        await dbContext.PalletMen.SafeExistAsync(i => i.Uid1C == dto.Id1C && i.Id != id, "Ошибка уникальности");

        PalletManEntity entity = await dbContext.PalletMen.SafeGetById(id, "Не найдено");
        WarehouseEntity warehouse  = await dbContext.Warehouses.SafeGetById(dto.WarehouseId, "Не найдено");
        await userManager.CanUserWorkWithProductionSiteAsync(warehouse.ProductionSiteId);

        dto.UpdateEntity(entity, warehouse);
        await dbContext.SaveChangesAsync();

        await LoadDefaultForeignKeysAsync(entity);
        return PalletManExpressions.ToDto.Compile().Invoke(entity);
    }

    #endregion

    #region Private

    private async Task LoadDefaultForeignKeysAsync(PalletManEntity entity) {
        await dbContext.Entry(entity).Reference(e => e.Warehouse).LoadAsync();
        await dbContext.Entry(entity.Warehouse).Reference(e => e.ProductionSite).LoadAsync();
    }

    #endregion
}