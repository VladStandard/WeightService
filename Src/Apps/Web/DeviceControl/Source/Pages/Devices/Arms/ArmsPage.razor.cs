using System.Security.Claims;
using DeviceControl.Source.Widgets.Section.Dialogs;
using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Arms;
using Ws.Domain.Services.Features.ProductionSites;
using Ws.Shared.Extensions;

namespace DeviceControl.Source.Pages.Devices.Arms;

public sealed partial class ArmsPage : SectionDataGridPageBase<Arm>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IArmService ArmService { get; set; } = default!;
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
            IsSeniorSupport = (await AuthorizationService.AuthorizeAsync(userPrincipal, PolicyEnum.SupportSenior))
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
        => await DialogService.ShowDialogAsync<ArmsCreateDialog>(
            new SectionDialogContentWithProductionSite<Arm> { Item = new(), ProductionSite = ProductionSite }
            , DialogParameters);

    protected override async Task OpenDataGridEntityModal(Arm item)
        => await OpenSectionModal<ArmsUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(Arm item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionLines}/{item.Uid.ToString()}");

    protected override IEnumerable<Arm> SetSqlSectionCast() =>
        ProductionSite.IsNew ? [] : ArmService.GetAllByProductionSite(ProductionSite)
            .OrderBy(item => item.Number);

    protected override IEnumerable<Arm> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [ArmService.GetItemByUid(itemUid)];
    }

    protected override Task DeleteItemAction(Arm item)
    {
        ArmService.Delete(item);
        return Task.CompletedTask;
    }

}