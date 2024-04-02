using DeviceControl.Source.Shared.Utils;
using DeviceControl.Source.Widgets.Section;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.ProductionSite;
using Ws.Domain.Services.Features.Warehouse;
using Ws.Shared.Resources;

namespace DeviceControl.Source.Pages.References.Warehouses;

public sealed partial class WarehousesUpdateForm : SectionFormBase<WarehouseEntity>
{
    #region Inject
    [Inject] private Redirector Redirector { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IProductionSiteService ProductionSiteService { get; set; } = default!;
    [Inject] private IWarehouseService WarehouseService { get; set; } = default!;

    #endregion

    private IEnumerable<ProductionSiteEntity> PlatformEntities { get; set; } = new List<ProductionSiteEntity>();

    protected override void OnInitialized()
    {
        PlatformEntities = ProductionSiteService.GetAll();
        base.OnInitialized();
    }

    protected override WarehouseEntity UpdateItemAction(WarehouseEntity item) =>
        WarehouseService.Update(item);

    protected override Task DeleteItemAction(WarehouseEntity item)
    {
        WarehouseService.Delete(item);
        return Task.CompletedTask;
    }
}

public class WarehousesUpdateFormValidator : AbstractValidator<WarehouseEntity>
{
    public WarehousesUpdateFormValidator()
    {
        RuleFor(item => item.Name).NotEmpty();
        RuleFor(item => item.ProductionSite).Custom((obj, context) =>
        {
            if (obj.IsNew)
                context.AddFailure("С объектом ProductionSite что-то не так");
        });
    }
}