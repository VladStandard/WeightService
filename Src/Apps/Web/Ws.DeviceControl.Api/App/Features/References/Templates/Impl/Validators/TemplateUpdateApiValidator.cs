using Microsoft.Extensions.Localization;
using Ws.Database.Entities.Zpl.Templates;
using Ws.DeviceControl.Api.App.Features.References.Templates.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.References.Templates.Impl.Models;
using Ws.DeviceControl.Api.App.Shared.Validators.Api;
using Ws.DeviceControl.Api.App.Shared.Validators.Api.Models;
using Ws.DeviceControl.Models.Features.References.Template.Commands;
using Ws.Shared.Resources;

namespace Ws.DeviceControl.Api.App.Features.References.Templates.Impl.Validators;

public class TemplateUpdateApiValidator(IStringLocalizer<WsDataResources> wsDataLocalizer, ErrorHelper errorHelper)
    : ApiUpdateValidator<TemplateEntity, TemplateUpdateDto, Guid>(errorHelper)
{
    public override async Task ValidateAsync(DbSet<TemplateEntity> dbSet, TemplateUpdateDto dto, Guid id)
    {
        UqTemplateProperties uqProperties = new(dto.Name);
        PredicateField<TemplateEntity> idPredicate = new(i => i.Id == id, string.Empty);
        await ValidateProperties(new TemplateUpdateValidator(wsDataLocalizer), dto);
        await ValidatePredicatesAsync(dbSet, TemplateExpressions.GetUqPredicates(uqProperties), idPredicate);
    }
}