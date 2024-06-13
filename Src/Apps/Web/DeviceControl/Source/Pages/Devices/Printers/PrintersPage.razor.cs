using System.Security.Claims;
using DeviceControl.Source.Widgets.Section.Dialogs;
using Ws.Domain.Models.Entities.Devices;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Users;
using Ws.Domain.Services.Features.Printers;
using Ws.Domain.Services.Features.ProductionSites;
using Ws.Domain.Services.Features.Users;
using Ws.Shared.Extensions;

namespace DeviceControl.Source.Pages.Devices.Printers;

public sealed partial class PrintersPage : SectionDataGridPageBase<Printer>
{
    #region Inject

    [CascadingParameter] private Task<AuthenticationState> AuthState { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IPrinterService PrinterService { get; set; } = default!;
    [Inject] private IUserService UserService { get; set; } = default!;
    [Inject] private IProductionSiteService ProductionSiteService { get; set; } = default!;
    [Inject] private IAuthorizationService AuthorizationService { get; set; } = default!;

    #endregion

    private User User { get; set; } = new();
    private ProductionSite ProductionSite { get; set; } = new();
    private List<ProductionSite> ProductionSiteEntities { get; set; } = [];
    private bool IsSeniorSupport { get; set; }
    private bool IsDeveloper { get; set; }

    protected override async Task OnInitializedAsync()
    {
        ClaimsPrincipal userPrincipal = (await AuthState).User;

        if (userPrincipal is { Identity.Name: not null })
        {
            User = UserService.GetItemByNameOrCreate(userPrincipal.Identity.Name);
            ProductionSite = User.ProductionSite ?? new();
            IsSeniorSupport = (await AuthorizationService.AuthorizeAsync(userPrincipal, PolicyEnum.SupportSenior)).Succeeded;
            IsDeveloper = (await AuthorizationService.AuthorizeAsync(userPrincipal, PolicyEnum.Developer)).Succeeded || ProductionSite.Uid.IsMax();
        }

        if (IsSeniorSupport)
            ProductionSiteEntities = ProductionSiteService.GetAll().ToList();

        if (!IsDeveloper)
            ProductionSiteEntities.RemoveAll(i => i.Uid.IsMax());

        await base.OnInitializedAsync();
    }

    protected override async Task OpenSectionCreateForm()
        => await DialogService.ShowDialogAsync<PrintersCreateDialog>(
            new SectionDialogContentWithProductionSite<Printer>
                { Item = new(), ProductionSite = ProductionSite }, DialogParameters);

    protected override async Task OpenDataGridEntityModal(Printer item)
        => await OpenSectionModal<PrintersUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(Printer item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionPrinters}/{item.Uid.ToString()}");

    protected override IEnumerable<Printer> SetSqlSectionCast() =>
        ProductionSite.IsNew ? [] : PrinterService.GetAllByProductionSite(ProductionSite);

    protected override IEnumerable<Printer> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [PrinterService.GetItemByUid(itemUid)];
    }

    protected override Task DeleteItemAction(Printer item)
    {
        PrinterService.Delete(item);
        return Task.CompletedTask;
    }
}