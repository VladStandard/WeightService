using Microsoft.Extensions.Localization;
using Ws.Database.Entities.Ref.Warehouses;
using Ws.DeviceControl.Api.App.Features.References.Warehouses.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.References.Warehouses.Impl.Models;
using Ws.DeviceControl.Api.App.Shared.Validators.Api;
using Ws.DeviceControl.Api.App.Shared.Validators.Api.Models;
using Ws.DeviceControl.Models.Features.References.Warehouses.Commands;
using Ws.Shared.Resources;

namespace Ws.DeviceControl.Api.App.Features.References.Warehouses.Impl.Validators;

public class WarehouseUpdateApiValidator(IStringLocalizer<WsDataResources> wsDataLocalizer, ErrorHelper errorHelper)
    : ApiUpdateValidator<WarehouseEntity, WarehouseUpdateDto, Guid>(errorHelper)
{
    public override async Task ValidateAsync(DbSet<WarehouseEntity> dbSet, WarehouseUpdateDto dto, Guid id)
    {
        UqWarehousesProperties uqProperties = new(dto.Name, dto.Id1C);
        PredicateField<WarehouseEntity> idPredicate = new(i => i.Id == id, string.Empty);

        await ValidateProperties(new WarehouseUpdateValidator(wsDataLocalizer), dto);
        await ValidatePredicatesAsync(dbSet, WarehouseExpressions.GetUqPredicates(uqProperties), idPredicate);
    }
}