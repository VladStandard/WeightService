using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.ProductionSite;
using Ws.Domain.Services.Features.Warehouse;

namespace DeviceControl.Source.Pages.References.Warehouses;

public sealed partial class WarehousesUpdateForm : SectionFormBase<Warehouse>
{
    #region Inject
    [Inject] private Redirector Redirector { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IProductionSiteService ProductionSiteService { get; set; } = default!;
    [Inject] private IWarehouseService WarehouseService { get; set; } = default!;

    #endregion

    private IEnumerable<ProductionSite> PlatformEntities { get; set; } = new List<ProductionSite>();

    protected override void OnInitialized()
    {
        PlatformEntities = ProductionSiteService.GetAll();
        base.OnInitialized();
    }

    protected override Warehouse UpdateItemAction(Warehouse item) =>
        WarehouseService.Update(item);

    protected override Task DeleteItemAction(Warehouse item)
    {
        WarehouseService.Delete(item);
        return Task.CompletedTask;
    }
}

public class WarehousesUpdateFormValidator : AbstractValidator<Warehouse>
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