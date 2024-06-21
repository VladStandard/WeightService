using System.Security.Claims;
using DeviceControl.Source.Widgets.Section.Dialogs;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.ProductionSites;
using Ws.Domain.Services.Features.Warehouses;
using Ws.Shared.Extensions;

namespace DeviceControl.Source.Pages.References.Warehouses;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class WarehousesPage : SectionDataGridPageBase<Warehouse>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IWarehouseService WarehouseService { get; set; } = default!;
    [Inject] private IProductionSiteService ProductionSiteService { get; set; } = default!;
    [Inject] private IAuthorizationService AuthorizationService { get; set; } = default!;

    #endregion

    [CascadingParameter] private Task<AuthenticationState> AuthState { get; set; } = default!;
    [CascadingParameter] private ProductionSite UserProductionSite { get; set; } = default!;

    private ProductionSite ProductionSite { get; set; } = new();
    private List<ProductionSite> ProductionSiteEntities { get; set; } = [];
    private bool IsSeniorSupport { get; set; }
    private bool IsDeveloper { get; set; }

    protected override async Task OnInitializedAsync()
    {
        ClaimsPrincipal userPrincipal = (await AuthState).User;
        ProductionSite = UserProductionSite;

        if (userPrincipal.Identity?.Name != null)
        {
            IsSeniorSupport = (await AuthorizationService.AuthorizeAsync(userPrincipal, PolicyEnum.SeniorSupport)).Succeeded;
            IsDeveloper = (await AuthorizationService.AuthorizeAsync(userPrincipal, PolicyEnum.Developer)).Succeeded || ProductionSite.Uid.IsMax();
        }

        if (IsSeniorSupport)
            ProductionSiteEntities = ProductionSiteService.GetAll().ToList();

        if (!IsDeveloper)
            ProductionSiteEntities.RemoveAll(i => i.Uid.IsMax());

        await base.OnInitializedAsync();
    }

    protected override async Task OpenSectionCreateForm()
        => await DialogService.ShowDialogAsync<WarehousesCreateDialog>(
            new SectionDialogContentWithProductionSite<Warehouse> { Item = new(), ProductionSite = ProductionSite }
            , DialogParameters);

    protected override async Task OpenDataGridEntityModal(Warehouse item)
        => await OpenSectionModal<WarehousesUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(Warehouse item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionWarehouses}/{item.Uid.ToString()}");

    protected override IEnumerable<Warehouse> SetSqlSectionCast() => WarehouseService.GetAllByProductionSite(ProductionSite);

    protected override IEnumerable<Warehouse> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [WarehouseService.GetItemByUid(itemUid)];
    }

    protected override Task DeleteItemAction(Warehouse item)
    {
        WarehouseService.Delete(item);
        return Task.CompletedTask;
    }
}