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
    UserManager userManager,
    ErrorMessages errorMessages
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
        PrinterEntity entity = await dbContext.Printers.SafeGetById(id, errorMessages.Localize(ErrorType.NotFound, "Printer"));
        await LoadDefaultForeignKeysAsync(entity);
        return PrinterExpressions.ToDto.Compile().Invoke(entity);
    }

    #endregion

    #region Commands

    public async Task<PrinterDto> CreateAsync(PrinterCreateDto dto)
    {
        await ValidateAsync(dto, createValidator);
        await dbContext.Printers.ThrowIfExistAsync(i => i.Name == dto.Name, errorMessages.Localize(ErrorType.Unique, "Name"));
        await dbContext.Printers.ThrowIfExistAsync(i => i.Ip == dto.Ip, errorMessages.Localize(ErrorType.Unique, "Ip"));

        ProductionSiteEntity productionSite = await dbContext.ProductionSites.SafeGetById(dto.ProductionSiteId, errorMessages.Localize(ErrorType.NotFound, "ProductionSite"));
        await userManager.CanUserWorkWithProductionSiteAsync(productionSite.Id);

        PrinterEntity entity = dto.ToEntity(productionSite);

        await dbContext.Printers.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return PrinterExpressions.ToDto.Compile().Invoke(entity);
    }

    public async Task<PrinterDto> UpdateAsync(Guid id, PrinterUpdateDto dto)
    {
        await ValidateAsync(dto, updateValidator);
        await dbContext.Printers.ThrowIfExistAsync(i => i.Name == dto.Name && i.Id != id, errorMessages.Localize(ErrorType.Unique, "Name"));
        await dbContext.Printers.ThrowIfExistAsync(i => i.Ip == dto.Ip && i.Id != id, errorMessages.Localize(ErrorType.Unique, "Ip"));

        PrinterEntity entity = await dbContext.Printers.SafeGetById(id, errorMessages.Localize(ErrorType.NotFound, "Printer"));
        await userManager.CanUserWorkWithProductionSiteAsync(entity.ProductionSiteId);

        dto.UpdateEntity(entity);
        await dbContext.SaveChangesAsync();

        return PrinterExpressions.ToDto.Compile().Invoke(entity);
    }

    public Task DeleteAsync(Guid id) => dbContext.Printers.SafeDeleteAsync(i => i.Id == id);

    #endregion

    #region Private

    private async Task LoadDefaultForeignKeysAsync(PrinterEntity entity)
    {
        await dbContext.Entry(entity).Reference(e => e.ProductionSite).LoadAsync();
    }

    #endregion
}