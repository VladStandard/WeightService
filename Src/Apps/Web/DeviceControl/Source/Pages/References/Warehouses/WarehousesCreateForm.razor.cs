using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.ProductionSites;
using Ws.Domain.Services.Features.Warehouses;

namespace DeviceControl.Source.Pages.References.Warehouses;

public sealed partial class WarehousesCreateForm : SectionFormBase<Warehouse>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IProductionSiteService ProductionSiteService { get; set; } = default!;
    [Inject] private IWarehouseService WarehouseService { get; set; } = default!;

    #endregion

    [Parameter, EditorRequired] public ProductionSite ProductionSite { get; set; } = new();

    protected override void OnInitialized()
    {
        DialogItem.ProductionSite = ProductionSite;
        base.OnInitialized();
    }

    protected override Warehouse CreateItemAction(Warehouse item) =>
        WarehouseService.Create(item);
}

public class WarehousesCreateFormValidator : AbstractValidator<Warehouse>
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