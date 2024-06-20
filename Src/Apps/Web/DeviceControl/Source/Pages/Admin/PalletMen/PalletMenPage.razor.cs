using System.Security.Claims;
using DeviceControl.Source.Widgets.Section.Dialogs;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Users;
using Ws.Domain.Services.Features.PalletMen;
using Ws.Domain.Services.Features.ProductionSites;
using Ws.Domain.Services.Features.Users;
using Ws.Shared.Extensions;
using Claim = System.Security.Claims.Claim;

namespace DeviceControl.Source.Pages.Admin.PalletMen;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class PalletMenPage : SectionDataGridPageBase<PalletMan>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IPalletManService PalletManService { get; set; } = default!;
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
            IsSeniorSupport = (await AuthorizationService.AuthorizeAsync(userPrincipal, PolicyEnum.SeniorSupport))
                .Succeeded;
            IsDeveloper = (await AuthorizationService.AuthorizeAsync(userPrincipal, PolicyEnum.Developer)).Succeeded ||
                          ProductionSite.Uid.IsMax();
        }

        if (IsSeniorSupport)
            ProductionSiteEntities = ProductionSiteService.GetAll().ToList();

        if (!IsDeveloper)
            ProductionSiteEntities.RemoveAll(i => i.Uid.IsMax());

        await base.OnInitializedAsync();
    }

    protected override async Task OpenSectionCreateForm()
        => await DialogService.ShowDialogAsync<PalletMenCreateDialog>(
            new SectionDialogContentWithProductionSite<PalletMan> { Item = new(), ProductionSite = ProductionSite }
            , DialogParameters);

    protected override async Task OpenDataGridEntityModal(PalletMan item)
        => await DialogService.ShowDialogAsync<PalletMenUpdateDialog>(
            new SectionDialogContentWithProductionSite<PalletMan> { Item = item, ProductionSite = ProductionSite }
            , DialogParameters);

    protected override async Task OpenItemInNewTab(PalletMan item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionPalletMen}/{item.Uid.ToString()}");

    protected override IEnumerable<PalletMan> SetSqlSectionCast() =>
        ProductionSite.IsNew ? [] : PalletManService.GetAllByProductionSite(ProductionSite);

    protected override IEnumerable<PalletMan> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [PalletManService.GetItemByUid(itemUid)];
    }

    protected override Task DeleteItemAction(PalletMan item)
    {
        PalletManService.Delete(item);
        return Task.CompletedTask;
    }
}