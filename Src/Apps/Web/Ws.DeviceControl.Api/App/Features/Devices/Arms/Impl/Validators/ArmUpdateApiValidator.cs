using Microsoft.Extensions.Localization;
using Ws.Database.Entities.Ref.Lines;
using Ws.DeviceControl.Api.App.Features.Devices.Arms.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.Devices.Arms.Impl.Models;
using Ws.DeviceControl.Api.App.Shared.Validators.Api;
using Ws.DeviceControl.Api.App.Shared.Validators.Api.Models;
using Ws.DeviceControl.Models.Features.Devices.Arms.Commands;
using Ws.Shared.Resources;

namespace Ws.DeviceControl.Api.App.Features.Devices.Arms.Impl.Validators;

public class ArmUpdateApiValidator(IStringLocalizer<WsDataResources> wsDataLocalizer, ErrorHelper errorHelper)
    : ApiUpdateValidator<LineEntity, ArmUpdateDto, Guid>(errorHelper)
{
    public override async Task ValidateAsync(DbSet<LineEntity> dbSet, ArmUpdateDto dto, Guid id)
    {
        UqArmProperties uqProperties = new(dto.SystemKey, dto.Name, dto.Number);
        PredicateField<LineEntity> idPredicate = new(i => i.Id == id, string.Empty);
        await ValidateProperties(new ArmUpdateValidator(wsDataLocalizer), dto);
        await ValidatePredicatesAsync(dbSet, ArmExpressions.GetUqPredicates(uqProperties), idPredicate);
    }
}