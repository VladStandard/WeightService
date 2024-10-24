using Microsoft.Extensions.Localization;
using Ws.Database.Entities.Ref.Printers;
using Ws.DeviceControl.Api.App.Features.Devices.Printers.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.Devices.Printers.Impl.Models;
using Ws.DeviceControl.Api.App.Shared.Validators.Api;
using Ws.DeviceControl.Api.App.Shared.Validators.Api.Models;
using Ws.DeviceControl.Models.Features.Devices.Printers.Commands;
using Ws.Shared.Resources;

namespace Ws.DeviceControl.Api.App.Features.Devices.Printers.Impl.Validators;

public class PrinterUpdateApiValidator(IStringLocalizer<WsDataResources> wsDataLocalizer, ErrorHelper errorHelper)
    : ApiUpdateValidator<PrinterEntity, PrinterUpdateDto, Guid>(errorHelper)
{
    public override async Task ValidateAsync(DbSet<PrinterEntity> dbSet, PrinterUpdateDto dto, Guid id)
    {
        UqPrinterProperties uqProperties = new(dto.Ip, dto.Name);
        PredicateField<PrinterEntity> idPredicate = new(i => i.Id == id, string.Empty);
        await ValidateProperties(new PrinterUpdateValidator(wsDataLocalizer), dto);
        await ValidatePredicatesAsync(dbSet, PrinterExpressions.GetUqPredicates(uqProperties), idPredicate);
    }
}