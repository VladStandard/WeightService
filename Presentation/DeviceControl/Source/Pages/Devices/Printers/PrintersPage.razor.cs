using System.Security.Claims;
using DeviceControl.Source.Shared.Localization;
using DeviceControl.Source.Shared.Utils;
using DeviceControl.Source.Widgets.Section;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Printer;
using Ws.Domain.Services.Features.User;
using Ws.Shared.Resources;

namespace DeviceControl.Source.Pages.Devices.Printers;

public sealed partial class PrintersPage : SectionDataGridPageBase<PrinterEntity>
{
    #region Inject

    [CascadingParameter] private Task<AuthenticationState> AuthState { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IPrinterService PrinterService { get; set; } = default!;
    [Inject] private IUserService UserService { get; set; } = default!;

    #endregion

    private UserEntity User { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        ClaimsPrincipal userClaims = (await AuthState).User;
        if (userClaims is { Identity.Name: not null })
            User = UserService.GetItemByNameOrCreate(userClaims.Identity.Name);
        await base.OnInitializedAsync();
    }

    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<PrintersCreateDialog>(new());

    protected override async Task OpenDataGridEntityModal(PrinterEntity item)
        => await OpenSectionModal<PrintersUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(PrinterEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionPrinters}/{item.Uid.ToString()}");

    protected override IEnumerable<PrinterEntity> SetSqlSectionCast() =>
        User.ProductionSite == null ? [] : PrinterService.GetAllByProductionSite(User.ProductionSite);

    protected override IEnumerable<PrinterEntity> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [PrinterService.GetItemByUid(itemUid)];
    }

    protected override Task DeleteItemAction(PrinterEntity item)
    {
        PrinterService.Delete(item);
        return Task.CompletedTask;
    }
}