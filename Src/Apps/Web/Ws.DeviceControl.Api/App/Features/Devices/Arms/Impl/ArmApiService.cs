using Ws.Database.EntityFramework.Entities.Ref.Lines;
using Ws.Database.EntityFramework.Entities.Ref.Printers;
using Ws.Database.EntityFramework.Entities.Ref.Warehouses;
using Ws.Database.EntityFramework.Entities.Ref1C.Plus;
using Ws.DeviceControl.Api.App.Features.Devices.Arms.Common;
using Ws.DeviceControl.Api.App.Features.Devices.Arms.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.Devices.Arms.Impl.Extensions;
using Ws.DeviceControl.Models.Features.Devices.Arms.Commands.Create;
using Ws.DeviceControl.Models.Features.Devices.Arms.Commands.Update;
using Ws.DeviceControl.Models.Features.Devices.Arms.Queries;

namespace Ws.DeviceControl.Api.App.Features.Devices.Arms.Impl;

internal sealed class ArmApiService(
    WsDbContext dbContext,
    ArmCreateValidator createValidator,
    ArmUpdateValidator updateValidator,
    UserHelper userHelper,
    ErrorHelper errorHelper
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
        LineEntity entity = await dbContext.Lines.SafeGetById(id, errorHelper.Localize(ErrorType.NotFound, "Line"));
        await LoadDefaultForeignKeysAsync(entity);
        return ArmExpressions.ToDto.Compile().Invoke(entity);
    }

    public async Task<List<PluArmDto>> GetArmPlus(Guid id)
    {
        LineEntity entity = await dbContext.Lines.SafeGetById(id, errorHelper.Localize(ErrorType.NotFound, "Line"));

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
        await dbContext.Lines.ThrowIfExistAsync(i => i.Name == dto.Name, errorHelper.Localize(ErrorType.Unique, "Name"));
        await dbContext.Lines.ThrowIfExistAsync(i => i.Number == dto.Number, errorHelper.Localize(ErrorType.Unique, "Number"));
        await dbContext.Lines.ThrowIfExistAsync(i => i.PcName == dto.PcName, errorHelper.Localize(ErrorType.Unique, "PcName"));

        WarehouseEntity warehouse = await dbContext.Warehouses.SafeGetById(dto.WarehouseId, errorHelper.Localize(ErrorType.NotFound, "Warehouse"));
        PrinterEntity printer = await dbContext.Printers.SafeGetById(dto.PrinterId, errorHelper.Localize(ErrorType.NotFound, "Printer"));
        LineEntity entity = dto.ToEntity(warehouse, printer);

        await userHelper.CanUserWorkWithProductionSiteAsync(warehouse.ProductionSiteId);

        await dbContext.Lines.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        await LoadDefaultForeignKeysAsync(entity);
        return ArmExpressions.ToDto.Compile().Invoke(entity);
    }

    public async Task<ArmDto> UpdateAsync(Guid id, ArmUpdateDto dto)
    {
        await ValidateAsync(dto, updateValidator);
        await dbContext.Lines.ThrowIfExistAsync(i => i.Name == dto.Name && i.Id != id, errorHelper.Localize(ErrorType.Unique, "Name"));
        await dbContext.Lines.ThrowIfExistAsync(i => i.Number == dto.Number && i.Id != id, errorHelper.Localize(ErrorType.Unique, "Number"));
        await dbContext.Lines.ThrowIfExistAsync(i => i.PcName == dto.PcName && i.Id != id, errorHelper.Localize(ErrorType.Unique, "PcName"));

        PrinterEntity printer = await dbContext.Printers.SafeGetById(dto.PrinterId, errorHelper.Localize(ErrorType.NotFound, "Printer"));
        WarehouseEntity warehouse = await dbContext.Warehouses.SafeGetById(dto.WarehouseId, errorHelper.Localize(ErrorType.NotFound, "Warehouse"));
        LineEntity entity = await dbContext.Lines.SafeGetById(id, errorHelper.Localize(ErrorType.NotFound, "Line"));

        await userHelper.CanUserWorkWithProductionSiteAsync(warehouse.ProductionSiteId);

        dto.UpdateEntity(entity, printer, warehouse);
        await dbContext.SaveChangesAsync();

        await LoadDefaultForeignKeysAsync(entity);
        return ArmExpressions.ToDto.Compile().Invoke(entity);
    }

    public async Task DeletePluAsync(Guid armId, Guid pluId)
    {
        LineEntity line = await dbContext.Lines
          .Include(l => l.Plus)
          .FirstOrDefaultAsync(l => l.Id == armId)
                ?? throw new("АРМ не найдено");

        PluEntity plu = await dbContext.Plus.SafeGetById(pluId, "ПЛУ не найдено");
        line.Plus.Remove(plu);
        await dbContext.SaveChangesAsync();
    }

    public async Task AddPluAsync(Guid armId, Guid pluId)
    {
        LineEntity line = await dbContext.Lines
          .Include(l => l.Plus)
          .FirstOrDefaultAsync(l => l.Id == armId)
                ?? throw new("АРМ не найдено");

        PluEntity plu = await dbContext.Plus.SafeGetById(pluId, "ПЛУ не найдено");

        if (line.Plus.Any(i => i.Id == pluId))
            return;

        line.Plus.Add(plu);

        await dbContext.SaveChangesAsync();
    }

    public Task DeleteAsync(Guid id) => dbContext.Lines.SafeDeleteAsync(i => i.Id == id);

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