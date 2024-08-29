using Ws.Database.EntityFramework.Entities.Ref.Printers;
using Ws.Database.EntityFramework.Entities.Ref.ProductionSites;
using Ws.DeviceControl.Api.App.Features.Devices.Printers.Common;
using Ws.DeviceControl.Api.App.Features.Devices.Printers.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.Devices.Printers.Impl.Extensions;
using Ws.DeviceControl.Models.Features.Devices.Printers.Commands.Create;
using Ws.DeviceControl.Models.Features.Devices.Printers.Commands.Update;
using Ws.DeviceControl.Models.Features.Devices.Printers.Queries;

namespace Ws.DeviceControl.Api.App.Features.Devices.Printers.Impl;

public class PrinterApiService(
    WsDbContext dbContext,
    PrinterCreateValidator createValidator,
    PrinterUpdateValidator updateValidator,
    UserManager userManager
    ) : ApiService, IPrinterService
{
    #region Queries

    public Task<List<PrinterDto>> GetAllByProductionSiteAsync(Guid productionSiteId)
    {
        return dbContext.Printers
            .AsNoTracking()
            .Where(i => i.ProductionSite.Id == productionSiteId)
            .Select(PrinterExpressions.ToDto)
            .OrderBy(i => i.Type).ThenBy(i => i.Name)
            .ToListAsync();
    }

    public Task<List<ProxyDto>> GetProxiesByProductionSiteAsync(Guid productionSiteId)
    {
        return dbContext.Printers
            .Where(i => i.ProductionSite.Id == productionSiteId)
            .Select(PrinterExpressions.ToProxy)
            .OrderBy(i => i.Name)
            .ToListAsync();
    }

    public async Task<PrinterDto> GetByIdAsync(Guid id)
    {
        PrinterEntity entity = await dbContext.Printers.SafeGetById(id, "Не найдено");
        await LoadDefaultForeignKeysAsync(entity);
        return PrinterExpressions.ToDto.Compile().Invoke(entity);
    }

    #endregion

    #region Commands

    public async Task<PrinterDto> CreateAsync(PrinterCreateDto dto)
    {
        await ValidateAsync(dto, createValidator);
        await dbContext.Printers.SafeExistAsync(i => i.Name == dto.Name, "Ошибка уникальности");
        await dbContext.Printers.SafeExistAsync(i => i.Ip == dto.Ip, "Ошибка уникальности");

        ProductionSiteEntity productionSite = await dbContext.ProductionSites.SafeGetById(dto.ProductionSiteId, "Не найдено");
        await userManager.CanUserWorkWithProductionSiteAsync(productionSite.Id);

        PrinterEntity entity = dto.ToEntity(productionSite);

        await dbContext.Printers.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return PrinterExpressions.ToDto.Compile().Invoke(entity);
    }

    public async Task<PrinterDto> UpdateAsync(Guid id, PrinterUpdateDto dto)
    {
        await ValidateAsync(dto, updateValidator);
        await dbContext.Printers.SafeExistAsync(i => i.Name == dto.Name && i.Id != id, "Ошибка уникальности");
        await dbContext.Printers.SafeExistAsync(i => i.Ip == dto.Ip && i.Id != id, "Ошибка уникальности");

        PrinterEntity entity = await dbContext.Printers.SafeGetById(id, "Не найдено");
        await userManager.CanUserWorkWithProductionSiteAsync(entity.ProductionSiteId);

        dto.UpdateEntity(entity);
        await dbContext.SaveChangesAsync();

        return PrinterExpressions.ToDto.Compile().Invoke(entity);
    }

    #endregion

    #region Private

    private async Task LoadDefaultForeignKeysAsync(PrinterEntity entity)
    {
        await dbContext.Entry(entity).Reference(e => e.ProductionSite).LoadAsync();
    }

    #endregion
}