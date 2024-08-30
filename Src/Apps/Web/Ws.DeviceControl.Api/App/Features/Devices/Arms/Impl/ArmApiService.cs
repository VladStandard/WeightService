using Ws.Database.EntityFramework.Entities.Ref.Lines;
using Ws.Database.EntityFramework.Entities.Ref.Printers;
using Ws.Database.EntityFramework.Entities.Ref.Warehouses;
using Ws.DeviceControl.Api.App.Features.Devices.Arms.Common;
using Ws.DeviceControl.Api.App.Features.Devices.Arms.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.Devices.Arms.Impl.Extensions;
using Ws.DeviceControl.Models.Features.Devices.Arms.Commands.Create;
using Ws.DeviceControl.Models.Features.Devices.Arms.Commands.Update;
using Ws.DeviceControl.Models.Features.Devices.Arms.Queries;
using Ws.Shared.Enums;
using Ws.Shared.Extensions;

namespace Ws.DeviceControl.Api.App.Features.Devices.Arms.Impl;

public class ArmApiService(
    WsDbContext dbContext,
    ArmCreateValidator createValidator,
    ArmUpdateValidator updateValidator,
    UserManager userManager
    ) : ApiService, IArmService
{
    #region Queries

    public Task<List<ArmDto>> GetAllByProductionSiteAsync(Guid productionSiteId)
    {
        return dbContext.Lines
            .AsNoTracking()
            .Where(i => i.Warehouse.ProductionSite.Id == productionSiteId)
            .Select(ArmExpressions.ToDto)
            .OrderBy(i => i.Name)
            .ToListAsync();
    }

    public async Task<ArmDto> GetByIdAsync(Guid id)
    {
        LineEntity entity = await dbContext.Lines.SafeGetById(id, "Не найдено");
        await LoadDefaultForeignKeysAsync(entity);
        return ArmExpressions.ToDto.Compile().Invoke(entity);
    }

    public async Task<List<PluArmDto>> GetArmPlus(Guid id)
    {
        LineEntity entity = await dbContext.Lines.SafeGetById(id, "Не найдено");

        bool? isWeightFilter = entity.Type switch
        {
            ArmType.Pc => false,
            ArmType.Tablet => true,
            _ => null
        };

        List<Guid> linePluId = await dbContext.Lines
            .AsNoTracking()
            .Where(i => i.Id == id)
            .SelectMany(i => i.Plus)
            .Select(i => i.Id)
            .ToListAsync();

       return await dbContext.Plus
           .AsNoTracking()
           .IfWhere(isWeightFilter != null, p => p.IsWeight == isWeightFilter)
           .OrderBy(i => i.Number)
           .Select(ArmExpressions.ToPluDto(linePluId))
           .ToListAsync();

    }

    #endregion

    #region Commands

    public async Task<ArmDto> CreateAsync(ArmCreateDto dto)
    {
        await ValidateAsync(dto, createValidator);
        await dbContext.Lines.ThrowIfExistAsync(i => i.Name == dto.Name, "Ошибка уникальности");
        await dbContext.Lines.ThrowIfExistAsync(i => i.Number == dto.Number, "Ошибка уникальности");
        await dbContext.Lines.ThrowIfExistAsync(i => i.PcName == dto.PcName, "Ошибка уникальности");

        WarehouseEntity warehouse = await dbContext.Warehouses.SafeGetById(dto.WarehouseId, "Не найдено");
        PrinterEntity printer = await dbContext.Printers.SafeGetById(dto.PrinterId, "Не найдено");
        LineEntity entity = dto.ToEntity(warehouse, printer);

        await userManager.CanUserWorkWithProductionSiteAsync(warehouse.ProductionSiteId);

        await dbContext.Lines.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        await LoadDefaultForeignKeysAsync(entity);
        return ArmExpressions.ToDto.Compile().Invoke(entity);
    }

    public async Task<ArmDto> UpdateAsync(Guid id, ArmUpdateDto dto)
    {
        await ValidateAsync(dto, updateValidator);
        await dbContext.Lines.ThrowIfExistAsync(i => i.Name == dto.Name && i.Id != id, "Ошибка уникальности");
        await dbContext.Lines.ThrowIfExistAsync(i => i.Number == dto.Number && i.Id != id, "Ошибка уникальности");
        await dbContext.Lines.ThrowIfExistAsync(i => i.PcName == dto.PcName && i.Id != id, "Ошибка уникальности");

        PrinterEntity printer = await dbContext.Printers.SafeGetById(dto.PrinterId, "Не найдено");
        WarehouseEntity warehouse = await dbContext.Warehouses.SafeGetById(dto.WarehouseId, "Не найдено");
        LineEntity entity = await dbContext.Lines.SafeGetById(id, "Не найдено");

        await userManager.CanUserWorkWithProductionSiteAsync(warehouse.ProductionSiteId);

        dto.UpdateEntity(entity, printer, warehouse);
        await dbContext.SaveChangesAsync();

        await LoadDefaultForeignKeysAsync(entity);
        return ArmExpressions.ToDto.Compile().Invoke(entity);
    }

    #endregion

    #region Private

    private async Task LoadDefaultForeignKeysAsync(LineEntity entity)
    {
        await dbContext.Entry(entity).Reference(e => e.Printer).LoadAsync();
        await dbContext.Entry(entity).Reference(e => e.Warehouse).LoadAsync();
        await dbContext.Entry(entity.Warehouse).Reference(e => e.ProductionSite).LoadAsync();
    }

    #endregion
}