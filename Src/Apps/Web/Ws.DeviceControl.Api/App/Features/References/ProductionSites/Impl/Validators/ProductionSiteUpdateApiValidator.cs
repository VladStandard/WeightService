using Microsoft.Extensions.Localization;
using Ws.Database.Entities.Ref.ProductionSites;
using Ws.DeviceControl.Api.App.Features.References.ProductionSites.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.References.ProductionSites.Impl.Models;
using Ws.DeviceControl.Api.App.Shared.Validators.Api;
using Ws.DeviceControl.Api.App.Shared.Validators.Api.Models;
using Ws.DeviceControl.Models.Features.References.ProductionSites.Commands;
using Ws.Shared.Resources;

namespace Ws.DeviceControl.Api.App.Features.References.ProductionSites.Impl.Validators;

public class ProductionSiteUpdateApiValidator(IStringLocalizer<WsDataResources> wsDataLocalizer, ErrorHelper errorHelper)
    : ApiUpdateValidator<ProductionSiteEntity, ProductionSiteUpdateDto, Guid>(errorHelper)
{
    public override async Task ValidateAsync(DbSet<ProductionSiteEntity> dbSet, ProductionSiteUpdateDto dto, Guid id)
    {
        UqProductionSiteProperties uqProperties = new(dto.Name);
        PredicateField<ProductionSiteEntity> idPredicate = new(i => i.Id == id, string.Empty);

        await ValidateProperties(new ProductionSiteUpdateValidator(wsDataLocalizer), dto);
        await ValidatePredicatesAsync(dbSet, ProductionSiteExpressions.GetUqPredicates(uqProperties), idPredicate);
    }
}