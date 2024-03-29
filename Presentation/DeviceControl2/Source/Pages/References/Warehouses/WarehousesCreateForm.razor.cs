using DeviceControl2.Source.Shared.Localization;
using DeviceControl2.Source.Widgets.Section;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.ProductionSite;
using Ws.Domain.Services.Features.Warehouse;
using Ws.Shared.Resources;

namespace DeviceControl2.Source.Pages.References.Warehouses;

public sealed partial class WarehousesCreateForm: SectionFormBase<WarehouseEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IProductionSiteService ProductionSiteService { get; set; } = default!;
    [Inject] private IWarehouseService WarehouseService { get; set; } = default!;

    #endregion

    private IEnumerable<ProductionSiteEntity> PlatformEntities { get; set; } = new List<ProductionSiteEntity>();

    protected override void OnInitialized()
    {
        DialogItem.ProductionSite.Name = Localizer["FormProductionSiteDefaultPlaceholder"];
        PlatformEntities = ProductionSiteService.GetAll();
        base.OnInitialized();
    }

    protected override WarehouseEntity CreateItemAction(WarehouseEntity item) =>
        WarehouseService.Create(item);
}

public class WarehousesCreateFormValidator : AbstractValidator<WarehouseEntity>
{
    public WarehousesCreateFormValidator()
    {
        RuleFor(item => item.Name).NotEmpty();
        RuleFor(item => item.ProductionSite).Custom((obj, context) =>
        {
            if (obj.IsNew)
                context.AddFailure("С объектом ProductionSite что-то не так");
        });
    }
}
