using Microsoft.Extensions.Localization;
using Ws.Database.Entities.Zpl.ZplResources;
using Ws.DeviceControl.Api.App.Features.References.TemplateResources.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.References.TemplateResources.Impl.Models;
using Ws.DeviceControl.Api.App.Shared.Validators.Api;
using Ws.DeviceControl.Api.App.Shared.Validators.Api.Models;
using Ws.DeviceControl.Models.Features.References.TemplateResources.Commands;
using Ws.Shared.Resources;

namespace Ws.DeviceControl.Api.App.Features.References.TemplateResources.Impl.Validators;

public class ZplResourceUpdateApiValidator(IStringLocalizer<WsDataResources> wsDataLocalizer, ErrorHelper errorHelper)
    : ApiUpdateValidator<ZplResourceEntity, ZplResourceUpdateDto, Guid>(errorHelper)
{
    public override async Task ValidateAsync(DbSet<ZplResourceEntity> dbSet, ZplResourceUpdateDto dto, Guid id)
    {
        UqZplResourceProperties uqProperties = new(dto.Name);
        PredicateField<ZplResourceEntity> idPredicate = new(i => i.Id == id, string.Empty);
        await ValidateProperties(new ZplResourceUpdateValidator(wsDataLocalizer), dto);
        await ValidatePredicatesAsync(dbSet, ZplResourceExpressions.GetUqPredicates(uqProperties), idPredicate);
    }
}