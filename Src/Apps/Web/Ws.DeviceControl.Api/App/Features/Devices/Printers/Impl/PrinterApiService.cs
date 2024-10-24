using Ws.Database.Entities.Ref.Printers;
using Ws.Database.Entities.Ref.ProductionSites;
using Ws.DeviceControl.Api.App.Features.Devices.Printers.Common;
using Ws.DeviceControl.Api.App.Features.Devices.Printers.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.Devices.Printers.Impl.Extensions;
using Ws.DeviceControl.Api.App.Features.Devices.Printers.Impl.Validators;
using Ws.DeviceControl.Models.Features.Devices.Printers.Commands;
using Ws.DeviceControl.Models.Features.Devices.Printers.Queries;

namespace Ws.DeviceControl.Api.App.Features.Devices.Printers.Impl;

internal sealed class PrinterApiService(
    WsDbContext dbContext,
    UserHelper userHelper,
    ErrorHelper errorHelper,
    PrinterCreateApiValidator createValidator,
    PrinterUpdateApiValidator updateValidator
    ) : IPrinterService
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
        PrinterEntity entity =
            await dbContext.Printers.SafeGetById(id, errorHelper.Localize(ErrorType.NotFound, "Printer"));

        return await GetPrinterDto(entity);
    }

    #endregion

    #region Commands

    public async Task<PrinterDto> CreateAsync(PrinterCreateDto dto)
    {
        await createValidator.ValidateAsync(dbContext.Printers, dto);

        ProductionSiteEntity productionSite = await dbContext.ProductionSites.SafeGetById(dto.ProductionSiteId, errorHelper.Localize(ErrorType.NotFound, "ProductionSite"));
        await userHelper.CanUserWorkWithProductionSiteAsync(productionSite.Id);

        PrinterEntity entity = dto.ToEntity(productionSite);

        await dbContext.Printers.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return PrinterExpressions.ToDto.Compile().Invoke(entity);
    }

    public async Task<PrinterDto> UpdateAsync(Guid id, PrinterUpdateDto dto)
    {
        await updateValidator.ValidateAsync(dbContext.Printers, dto, id);

        PrinterEntity entity = await dbContext.Printers.SafeGetById(id, errorHelper.Localize(ErrorType.NotFound, "Printer"));
        await userHelper.CanUserWorkWithProductionSiteAsync(entity.ProductionSiteId);

        dto.UpdateEntity(entity);
        await dbContext.SaveChangesAsync();

        return PrinterExpressions.ToDto.Compile().Invoke(entity);
    }

    public Task DeleteAsync(Guid id) => dbContext.Printers.SafeDeleteAsync(i => i.Id == id);

    #endregion

    #region Private

    private async Task<PrinterDto> GetPrinterDto(PrinterEntity printer)
    {
        await dbContext.Entry(printer).Reference(e => e.ProductionSite).LoadAsync();
        return PrinterExpressions.ToDto.Compile().Invoke(printer);
    }

    #endregion
}